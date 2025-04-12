using DamLoad.Classify.Api.V1.Classifiers.Mappers;
using DamLoad.Classify.Api.V1.Classifiers.Requests;
using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Api.V1.Schemes.Mappers;
using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints;

public class GetClassifier : Endpoint<GetClassifierRequest, ClassifierResponse, ClassifierMapper>
{
    private readonly IClassifierService _service;

    public GetClassifier(IClassifierService service) => _service = service;

    public override void Configure()
    {
        Get("/api/v1/classify/classifiers/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetClassifierRequest req, CancellationToken ct)
    {
        var entity = await _service.GetByIdAsync(req.Id);
        if (entity is null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(Map.FromEntity(entity));
    }
}