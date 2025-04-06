using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Collections.Endpoints
{
    public class DeleteCollection : EndpointWithoutRequest
    {
        private readonly ICollectionService _service;

        public DeleteCollection(ICollectionService service) => _service = service;

        public override void Configure()
        {
            Delete("/api/v1/collections/{collectionId:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var collectionId = Route<Guid>("collectionId");
            await _service.DeleteAsync(collectionId);
            await SendOkAsync(ct);
        }
    }

}
