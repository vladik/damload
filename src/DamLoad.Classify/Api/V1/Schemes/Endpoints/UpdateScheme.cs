using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Api.V1.Schemes.Validators;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class UpdateScheme : Endpoint<UpdateSchemeRequest, SchemeResponse, SchemeMapper>
    {
        private readonly ISchemeService _schemeService;

        public UpdateScheme(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Put("/api/v1/classify/schemes/{id:guid}");
            AllowAnonymous();
            Validator<UpdateSchemeValidator>();
        }

        public override async Task HandleAsync(UpdateSchemeRequest req, CancellationToken ct)
        {
            var entity = Map.ToEntity(req);
            await _schemeService.UpdateAsync(entity);
            await SendAsync(Map.FromEntity(entity));
        }
    }
}