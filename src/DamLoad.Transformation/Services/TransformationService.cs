using DamLoad.Transformation.Providers;
using DamLoad.Transformation.Services;

namespace DamLoad.Transformation
{
    public class TransformationService : ITransformationService
    {
        private readonly IEnumerable<ITransformationProvider> _providers;

        public TransformationService(IEnumerable<ITransformationProvider> providers)
        {
            _providers = providers;
        }

        public async Task<TransformationResult> TransformAsync(TransformationRequest request)
        {
            var provider = _providers.FirstOrDefault(p => p.Supports(request.AssetType));
            if (provider == null)
                throw new NotSupportedException($"No provider available for asset type {request.AssetType}");

            return await provider.TransformAsync(request);
        }
    }
}
