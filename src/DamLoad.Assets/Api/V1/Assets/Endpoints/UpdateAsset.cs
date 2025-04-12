using DamLoad.Assets.Api.V1.Assets.Mappers;
using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Api.V1.Assets.Validators;
using DamLoad.Assets.Repositories;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints;

public class UpdateAsset : Endpoint<UpdateAssetRequest, UpdateAssetResponse, UpdateAssetMapper>
{
    private readonly IAssetRepository _repository;

    public UpdateAsset(IAssetRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Put("/api/v1/assets/{id:guid}");
        Validator<UpdateAssetValidator>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAssetRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Map.UpdateEntity(req, entity);

        await _repository.UpdateAsync(entity);

        await SendAsync(Map.FromEntity(entity), cancellation: ct);
    }
}
