
using DamLoad.Abstractions.Enums;

namespace DamLoad.Transformation.Providers
{
    public interface ITransformationProvider
    {
        string ProviderName { get; }
        Task<TransformationResult> TransformAsync(TransformationRequest request);
        bool Supports(AssetType assetType);
    }

}
