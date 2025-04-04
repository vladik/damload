using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Api.V1.Assets.Validators;
using DamLoad.Assets.Repositories;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints;

public class ListAssets : Endpoint<ListAssetsRequest, List<AssetResponse>>
{
    private readonly IAssetRepository _repository;

    public ListAssets(IAssetRepository repository) => _repository = repository;

    public override void Configure()
    {
        Get("/api/v1/assets/page/{PageNumber:int}/limit/{PageSize:int}");
        AllowAnonymous();
        Validator<ListAssetsValidator>();
    }

    public override async Task HandleAsync(ListAssetsRequest req, CancellationToken ct)
    {
        var allAssets = await _repository.GetAllAsync();
        var paginated = allAssets
            .Skip((req.PageNumber - 1) * req.PageSize)
            .Take(req.PageSize)
            .Select(AssetResponse.FromEntity)
            .ToList();

        await SendAsync(paginated);
    }
}
