using DamLoad.Classify.Api.V1.Classifications.Requests;
using DamLoad.Classify.Api.V1.Classifications.Validators;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Endpoints;

public class DeleteClassification : Endpoint<DeleteClassificationRequest>
{
    private readonly IClassificationService _service;

    public DeleteClassification(IClassificationService service) => _service = service;

    public override void Configure()
    {
        Delete("/api/v1/classify/classifications");
        AllowAnonymous();
        Validator<DeleteClassificationValidator>();
    }

    public override async Task HandleAsync(DeleteClassificationRequest req, CancellationToken ct)
    {
        await _service.RemoveAsync(req.ResourceId, req.ClassifierIds);
        await SendOkAsync();
    }
}