
using DamLoad.Core.Enums;

namespace DamLoad.Transformation.Providers
{
    public interface ITransformationProvider
    {
        Task<TransformationResult> TransformAsync(TransformationRequest request);
        bool Supports(AssetType assetType);
    }

}
