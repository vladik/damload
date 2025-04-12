using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Endpoints
{
    public class DeleteScheme : Endpoint<DeleteSchemeRequest>
    {
        private readonly ISchemeService _schemeService;

        public DeleteScheme(ISchemeService schemeService) => _schemeService = schemeService;

        public override void Configure()
        {
            Delete("/api/v1/classify/schemes/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteSchemeRequest req, CancellationToken ct)
        {
            await _schemeService.DeleteAsync(req.Id);
            await SendOkAsync();
        }
    }
}
