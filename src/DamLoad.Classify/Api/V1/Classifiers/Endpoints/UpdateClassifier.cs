using DamLoad.Classify.Api.V1.Classifiers.Mappers;
using DamLoad.Classify.Api.V1.Classifiers.Requests;
using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints;
public class UpdateClassifier : Endpoint<UpdateClassifierRequest, ClassifierResponse, ClassifierMapper>
{
    private readonly IClassifierService _service;

    public UpdateClassifier(IClassifierService service) => _service = service;

    public override void Configure()
    {
        Put("/api/v1/classify/classifiers/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClassifierRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var existing = await _service.GetByIdAsync(id);
        if (existing is null)
        {
            await SendNotFoundAsync();
            return;
        }

        existing.Slug = req.Slug;
        existing.Label = req.Label;
        existing.Properties = req.Properties;
        existing.SortOrder = req.SortOrder;
        existing.UpdatedAt = DateTime.UtcNow;

        await _service.UpdateAsync(existing);
        await SendAsync(Map.FromEntity(existing));
    }
}