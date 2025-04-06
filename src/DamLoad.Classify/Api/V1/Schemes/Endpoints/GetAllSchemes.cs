using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Entities;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class GetAllSchemes : EndpointWithoutRequest<List<SchemeResponse>, SchemeListMapper>
    {
        private readonly ISchemeService _schemeService;

        public GetAllSchemes(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Get("/api/v1/classify/schemes");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _schemeService.GetAllAsync();
            await SendMappedAsync(result);
        }
    }
}
