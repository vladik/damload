using DamLoad.Api.Features.Assets.Mappers;
using DamLoad.Api.Features.Assets.Requests;
using DamLoad.Api.Features.Assets.Responses;
using FastEndpoints;

namespace DamLoad.Api.Features.Assets.Endpoints
{
    public class GetAssetEndpoint : Endpoint<AssetRequest, AssetResponse, AssetMapper>
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
