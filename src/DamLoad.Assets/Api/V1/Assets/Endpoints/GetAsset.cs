using DamLoad.Assets.Api.V1.Assets.Mappers;
using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Repositories;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints;

public class GetAsset : Endpoint<GetAssetRequest, GetAssetResponse, GetAssetMapper>
{
    private readonly IAssetRepository _repository;

    public GetAsset(IAssetRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/api/v1/assets/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAssetRequest req, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(req.Id);
        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(Map.FromEntity(entity), cancellation: ct);
    }
}