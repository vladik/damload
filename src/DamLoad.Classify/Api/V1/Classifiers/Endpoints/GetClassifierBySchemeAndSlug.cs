using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints
{
    public class GetClassifierBySchemeAndSlug : EndpointWithoutRequest<ClassifierResponse>
    {
        private readonly IClassifierService _service;

        public GetClassifierBySchemeAndSlug(IClassifierService service) => _service = service;

        public override void Configure()
        {
            Get("/api/v1/classify/classifiers/scheme/{schemeId:guid}/slug/{slug}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var schemeId = Route<Guid>("schemeId");
            var slug = Route<string>("slug");
            var entity = await _service.GetBySchemeIdAndSlugAsync(schemeId, slug!);
            if (entity is null)
            {
                await SendNotFoundAsync();
                return;
            }
            await SendAsync(new ClassifierResponse
            {
                Id = entity.Id,
                SchemeId = entity.SchemeId,
                Slug = entity.Slug,
                Label = entity.Label,
                Properties = entity.Properties,
                SortOrder = entity.SortOrder,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            });
        }
    }
}
