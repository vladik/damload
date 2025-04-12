using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class GetSchemeBySlug : Endpoint<GetSchemeBySlugRequest, SchemeResponse, SchemeMapper>
    {
        private readonly ISchemeService _schemeService;

        public GetSchemeBySlug(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Get("/api/v1/classify/schemes/slug/{slug}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetSchemeBySlugRequest req, CancellationToken ct)
        {
            var scheme = await _schemeService.GetBySlugAsync(req.Slug);
            if (scheme is null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(Map.FromEntity(scheme));
        }
    }

}
