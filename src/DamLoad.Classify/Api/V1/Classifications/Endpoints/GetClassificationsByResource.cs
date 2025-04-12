using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Api.V1.Classifications.Mappers;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Endpoints;

public class GetClassificationsByResource : EndpointWithoutRequest<GetClassificationsByResourceResponse, ListClassificationMapper>
{
    private readonly IClassificationService _service;

    public GetClassificationsByResource(IClassificationService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/api/v1/classify/classifications/resource/{resourceId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var resourceId = Route<string>("resourceId");
        var result = await _service.GetByResourceIdAsync(resourceId!);
        await SendAsync(Map.FromEntity(result));
    }
}