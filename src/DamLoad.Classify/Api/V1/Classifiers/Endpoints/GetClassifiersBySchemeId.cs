using DamLoad.Classify.Api.V1.Classifiers.Mappers;
using DamLoad.Classify.Api.V1.Classifiers.Requests;
using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints
{
    public class GetClassifiersBySchemeId : Endpoint<GetClassifiersBySchemeIdRequest, List<ClassifierResponse>, ClassifierMapper>
    {
        private readonly IClassifierService _service;

        public GetClassifiersBySchemeId(IClassifierService service) => _service = service;

        public override void Configure()
        {
            Get("/api/v1/classify/classifiers/scheme/{schemeId:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetClassifiersBySchemeIdRequest req, CancellationToken ct)
        {
            var list = await _service.GetBySchemeIdAsync(req.SchemeId);
            await SendAsync(list.Select(Map.FromEntity).ToList());
        }
    }
}
