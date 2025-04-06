using DamLoad.Assets.Api.V1.Collections.Requests;
using DamLoad.Assets.Api.V1.Collections.Validators;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Collections.Endpoints
{
    public class RenameCollection : Endpoint<RenameCollectionRequest>
    {
        private readonly ICollectionService _service;

        public RenameCollection(ICollectionService service) => _service = service;

        public override void Configure()
        {
            Put("/api/v1/collections/{collectionId:guid}/rename");
            AllowAnonymous();
            Validator<RenameCollectionValidator>();
        }

        public override async Task HandleAsync(RenameCollectionRequest req, CancellationToken ct)
        {
            await _service.RenameAsync(req.CollectionId, req.NewName);
            await SendOkAsync(ct);
        }
    }

}
