using DamLoad.Assets.Api.V1.Collections.Mappers;
using DamLoad.Assets.Api.V1.Collections.Requests;
using DamLoad.Assets.Api.V1.Collections.Responses;
using DamLoad.Assets.Api.V1.Collections.Validators;
using DamLoad.Assets.Api.V1.Tags.Mappers;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Collections.Endpoints
{
    public class CreateCollection : Endpoint<CreateCollectionRequest, CollectionResponse, CollectionMapper>
    {
        private readonly ICollectionService _service;

        public CreateCollection(ICollectionService service) => _service = service;

        public override void Configure()
        {
            Post("/api/v1/collections");
            AllowAnonymous();
            Validator<CreateCollectionValidator>();
        }

        public override async Task HandleAsync(CreateCollectionRequest req, CancellationToken ct)
        {
            var entity = Map.ToEntity(req);
            await _service.AddAsync(entity);
            await SendAsync(Map.FromEntity(entity));
        }
    }

}
