using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints;

public class GetClassifier : EndpointWithoutRequest<ClassifierResponse>
{
    private readonly IClassifierService _service;

    public GetClassifier(IClassifierService service) => _service = service;

    public override void Configure()
    {
        Get("/api/v1/classify/classifiers/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var entity = await _service.GetByIdAsync(id);
        if (entity is null)
        {
            await SendNotFoundAsync();
            return;
        }
        var response = new ClassifierResponse
        {
            Id = entity.Id,
            SchemeId = entity.SchemeId,
            Slug = entity.Slug,
            Label = entity.Label,
            Properties = entity.Properties,
            SortOrder = entity.SortOrder,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
        await SendAsync(response);
    }
}