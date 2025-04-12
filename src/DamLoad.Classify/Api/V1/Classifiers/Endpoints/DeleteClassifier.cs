using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints;
public class DeleteClassifier : EndpointWithoutRequest
{
    private readonly IClassifierService _service;

    public DeleteClassifier(IClassifierService service) => _service = service;

    public override void Configure()
    {
        Delete("/api/v1/classify/classifiers/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        await _service.DeleteAsync(id);
        await SendOkAsync();
    }
}