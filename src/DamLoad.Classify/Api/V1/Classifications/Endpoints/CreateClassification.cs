using DamLoad.Classify.Api.V1.Classifications.Mappers;
using DamLoad.Classify.Api.V1.Classifications.Requests;
using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Api.V1.Classifications.Validators;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Endpoints;

public class CreateClassification : Endpoint<CreateClassificationRequest, CreateClassificationResponse, ClassificationMapper>
{
    private readonly IClassificationService _service;

    public CreateClassification(IClassificationService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Post("/api/v1/classify/classifications");
        AllowAnonymous();
        Validator<CreateClassificationValidator>();
    }

    public override async Task HandleAsync(CreateClassificationRequest req, CancellationToken ct)
    {
        var assigned = await _service.AssignAsync(req.ResourceId, req.ClassifierIds);
        await SendAsync(new CreateClassificationResponse
        {
            ResourceId = req.ResourceId,
            AssignedClassifierIds = assigned
        });
    }
}