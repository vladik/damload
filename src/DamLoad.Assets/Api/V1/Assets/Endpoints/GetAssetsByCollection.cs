using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Repositories;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints
{
    public class GetAssetsByCollection : EndpointWithoutRequest<List<AssetResponse>>
    {
        private readonly IAssetRepository _repository;

        public GetAssetsByCollection(IAssetRepository repository) => _repository = repository;

        public override void Configure()
        {
            Get("/api/v1/assets/collection/{collectionId:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var collectionId = Route<Guid>("collectionId");
            var assets = await _repository.GetAssetsByCollection(collectionId);
            await SendAsync(assets.Select(AssetResponse.FromEntity).ToList());
        }
    }
}
