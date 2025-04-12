using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class ListSchemes : EndpointWithoutRequest<List<SchemeResponse>, ListSchemeMapper>
    {
        private readonly ISchemeService _schemeService;

        public ListSchemes(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Get("/api/v1/classify/schemes");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var entities = await _schemeService.GetAllAsync();
            var response = Map.FromEntity(entities);
            await SendAsync(response);
        }
    }
}
