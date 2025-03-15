using DamLoad.Transformation.Providers;
using DamLoad.Transformation.Services;

namespace DamLoad.Transformation.Services
{
    public class TransformationService : ITransformationService
    {
        private readonly Dictionary<string, ITransformationProvider> _providers;

        public TransformationService(IEnumerable<ITransformationProvider> providers)
        {
            _providers = providers.ToDictionary(p => p.ProviderName, StringComparer.OrdinalIgnoreCase);
        }

        public ITransformationProvider GetProvider(string providerName)
        {
            if (_providers.TryGetValue(providerName, out var provider))
                return provider;

            throw new NotSupportedException($"Transformation provider '{providerName}' is not registered.");
        }

        public async Task<TransformationResult> TransformAsync(TransformationRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Provider))
            {
                if (_providers.TryGetValue(request.Provider, out var specificProvider))
                    return await specificProvider.TransformAsync(request);
            }

            var fallbackProvider = _providers.Values.FirstOrDefault(p => p.Supports(request.AssetType));
            if (fallbackProvider == null)
                throw new NotSupportedException($"No transformation provider available for asset type {request.AssetType}.");

            return await fallbackProvider.TransformAsync(request);
        }
    }

}
