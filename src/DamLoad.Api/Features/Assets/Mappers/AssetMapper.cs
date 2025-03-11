using DamLoad.Api.Features.Assets.Requests;
using DamLoad.Api.Features.Assets.Responses;
using DamLoad.Core.Contracts.Models;
using FastEndpoints;

namespace DamLoad.Api.Features.Assets.Mappers
{
    public class AssetMapper : Mapper<AssetRequest, AssetResponse, AssetModel>
    {
        public override AssetModel ToEntity(AssetRequest r) => new()
        {
           Name = r.Inp
        };

        public override AssetResponse FromEntity(AssetModel e) => new()
        {
            Message = e.Name
        };
    }
}
