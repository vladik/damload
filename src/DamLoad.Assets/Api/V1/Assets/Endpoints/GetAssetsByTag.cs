using FastEndpoints;
using DamLoad.Assets.Repositories;
using DamLoad.Assets.Api.V1.Assets.Responses;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints;

public class GetAssetsByTag : EndpointWithoutRequest<List<AssetResponse>>
{
    private readonly IAssetRepository _repository;

    public GetAssetsByTag(IAssetRepository repository) => _repository = repository;

    public override void Configure()
    {
        Get("/api/v1/assets/tag/{tagId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var tagId = Route<Guid>("tagId");
        var assets = await _repository.GetAssetsByTag(tagId);
        await SendAsync(assets.Select(AssetResponse.FromEntity).ToList());
    }
}