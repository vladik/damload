using DamLoad.Abstractions.Models;
using DamLoad.Assets.Api.Assets.Requests;
using DamLoad.Assets.Api.Assets.Responses;
using FastEndpoints;

namespace DamLoad.Assets.Api.Assets.Mappers
{
    public class AssetMapper : Mapper<AssetRequest, AssetResponse, AssetModel>
    {
        public override AssetModel ToEntity(AssetRequest r) => new()
        {
           //Name = r.Inp
        };

        public override AssetResponse FromEntity(AssetModel e) => new()
        {
            //Message = e.Name
        };
    }
}
