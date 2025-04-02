using DamLoad.Assets.Api.Assets.Requests;
using DamLoad.Assets.Api.Assets.Responses;
using FastEndpoints;

namespace DamLoad.Assets.Api.Assets.Endpoints
{
    public class GetAsset : Endpoint<AssetRequest, AssetResponse, DamLoad.Assets.Api.Assets.Mappers.AssetMapper>
    {
        public override void Configure()
        {
            Post("/api/articles");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AssetRequest req, CancellationToken ct)
        {
            //AssetModel model = Map.ToEntity(req);
            //Response = Map.FromEntity(model);
            await SendAsync(Response, cancellation: ct);
        }
    }
}
