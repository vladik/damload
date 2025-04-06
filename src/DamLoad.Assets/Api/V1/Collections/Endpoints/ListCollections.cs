using DamLoad.Assets.Api.V1.Collections.Mappers;
using DamLoad.Assets.Api.V1.Collections.Responses;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Collections.Endpoints
{
    public class ListCollections : EndpointWithoutRequest<List<CollectionResponse>, CollectionListMapper>
    {
        private readonly ICollectionService _service;

        public ListCollections(ICollectionService service) => _service = service;

        public override void Configure()
        {
            Get("/api/v1/collections");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _service.GetAllAsync();
            await SendMappedAsync(result);
        }
    }

}
