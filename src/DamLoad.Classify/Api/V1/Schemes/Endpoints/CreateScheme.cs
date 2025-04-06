using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Api.V1.Schemes.Validators;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class CreateScheme : Endpoint<CreateSchemeRequest, SchemeResponse, SchemeMapper>
    {
        private readonly ISchemeService _schemeService;

        public CreateScheme(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Post("/api/v1/classify/schemes");
            AllowAnonymous();
            Validator<CreateSchemeValidator>();
        }

        public override async Task HandleAsync(CreateSchemeRequest req, CancellationToken ct)
        {
            var entity = Map.ToEntity(req);
            await _schemeService.AddAsync(entity);
            await SendAsync(Map.FromEntity(entity));
        }
    }
}
