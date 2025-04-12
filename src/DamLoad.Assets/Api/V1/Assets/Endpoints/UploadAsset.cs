using DamLoad.Abstractions.Events;
using DamLoad.Abstractions.Models;
using DamLoad.Abstractions.Workflow.Providers;
using DamLoad.Assets.Api.V1.Assets.Mappers;
using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Api.V1.Assets.Validators;
using DamLoad.Assets.Repositories;
using DamLoad.Core;
using DamLoad.Data.Storage;
using FastEndpoints;
using IEventBus = DamLoad.Abstractions.Events.IEventBus;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints;

public class UploadAsset : Endpoint<UploadAssetRequest, UploadAssetResponse, UploadAssetMapper>
{
    private readonly IAssetRepository _repository;
    private readonly IWorkflowStatusProvider _workflowProvider;
    private readonly IEventBus _eventBus;
    private readonly IStorageProvider _storage;

    public UploadAsset(
        IAssetRepository repository,
        IWorkflowStatusProvider workflowProvider,
        IEventBus eventBus,
        IStorageProvider storage)
    {
        _repository = repository;
        _workflowProvider = workflowProvider;
        _eventBus = eventBus;
        _storage = storage;
    }

    public override void Configure()
    {
        Post("/api/v1/assets/upload");
        AllowAnonymous();
        AllowFileUploads();
        Validator<UploadAssetValidator>();
    }

    public override async Task HandleAsync(UploadAssetRequest req, CancellationToken ct)
    {
        var file = req.File;

        var status = _workflowProvider.GetDefaultStatus("damload.assets");

        var assetId = Guid.NewGuid();
        var publicId = Common.GeneratePublicId(); // Secure, base32
        var assetName = $"{publicId}.{req.Extension}";

        await using var stream = file.OpenReadStream();
        await _storage.UploadAsync(stream, assetName, req.ContentType, status);

        var entity = Map.ToEntity(req);
        entity.Id = assetId;
        entity.PublicId = publicId;
        entity.Url = assetName;
        entity.Status = status;
        entity.ContentType = req.ContentType;
        entity.Extension = req.Extension;

        await _repository.AddAsync(entity);

        await _eventBus.PublishAsync(new EntityEvent<AssetModel>
        {
            Identifier = "damload.assets:created",
            Data = DamLoad.Assets.Mappers.GetAssetMapper.ToModel(entity)
        });

        await SendAsync(Map.FromEntity(entity), cancellation: ct);
    }
}
