using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints
{
    public class GetClassifierBySlug : EndpointWithoutRequest<ClassifierResponse>
    {
        private readonly IClassifierService _service;

        public GetClassifierBySlug(IClassifierService service) => _service = service;

        public override void Configure()
        {
            Get("/api/v1/classify/classifiers/slug/{slug}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var slug = Route<string>("slug");
            var entity = await _service.GetBySlugAsync(slug!);
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
