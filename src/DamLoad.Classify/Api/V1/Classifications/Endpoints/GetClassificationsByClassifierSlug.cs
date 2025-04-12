using DamLoad.Classify.Api.V1.Classifications.Mappers;
using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Endpoints;

public class GetClassificationsByClassifierSlug : EndpointWithoutRequest<GetClassificationsByClassifierResponse>
{
    private readonly IClassificationService _service;

    public GetClassificationsByClassifierSlug(IClassificationService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/api/v1/classify/classifications/classifier/slug/{slug}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var slug = Route<string>("slug");
        var result = await _service.GetResourceIdsByClassifierSlugAsync(slug!);

        await SendAsync(new GetClassificationsByClassifierResponse
        {
            ResourceIds = result
        });
    }
}
