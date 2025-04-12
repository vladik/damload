using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class GetScheme : Endpoint<GetSchemeRequest, SchemeResponse, SchemeMapper>
    {
        private readonly ISchemeService _schemeService;

        public GetScheme(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Get("/api/v1/classify/schemes/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetSchemeRequest req, CancellationToken ct)
        {
            var scheme = await _schemeService.GetByIdAsync(req.Id);
            if (scheme is null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(Map.FromEntity(scheme));
        }
    }

}
