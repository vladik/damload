using DamLoad.Classify.Api.V1.Classifiers.Mappers;
using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints;
public class ListClassifiers : EndpointWithoutRequest<List<ClassifierResponse>, ListClassifierMapper>
{
    private readonly IClassifierService _classifierService;

    public ListClassifiers(IClassifierService classifierService) => _classifierService = classifierService;

    public override void Configure()
    {
        Get("/api/v1/classify/classifiers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var entities = await _classifierService.GetAllAsync();
        var response = Map.FromEntity(entities);
        await SendAsync(response);
    }
}