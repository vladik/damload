using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Repositories;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints
{
    public class GetAssetsByFolder : EndpointWithoutRequest<List<AssetResponse>>
    {
        private readonly IAssetRepository _repository;

        public GetAssetsByFolder(IAssetRepository repository) => _repository = repository;

        public override void Configure()
        {
            Get("/api/v1/assets/folder/{folderId:guid?}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var folderId = Route<Guid?>("folderId");
            var assets = await _repository.GetAssetsByFolder(folderId);
            await SendAsync(assets.Select(AssetResponse.FromEntity).ToList());
        }
    }
}
