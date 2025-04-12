using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Endpoints;

public class GetClassificationsByClassifier : EndpointWithoutRequest<GetClassificationsByClassifierResponse>
{
    private readonly IClassificationService _service;

    public GetClassificationsByClassifier(IClassificationService service) => _service = service;

    public override void Configure()
    {
        Get("/api/v1/classify/classifications/classifier/{classifierId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var classifierId = Route<Guid>("classifierId");
        var result = await _service.GetResourceIdsByClassifierAsync(classifierId);

        await SendAsync(new GetClassificationsByClassifierResponse
        {
            ResourceIds = result
        });
    }
}