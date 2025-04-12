using DamLoad.Classify.Api.V1.Classifiers.Mappers;
using DamLoad.Classify.Api.V1.Classifiers.Requests;
using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Services;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Endpoints
{
    public class CreateClassifier : Endpoint<CreateClassifierRequest, ClassifierResponse, ClassifierMapper>
    {
        private readonly IClassifierService _service;

        public CreateClassifier(IClassifierService service) => _service = service;

        public override void Configure()
        {
            Post("/api/v1/classify/classifiers");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateClassifierRequest req, CancellationToken ct)
        {
            var entity = Map.ToEntity(req);
            await _service.AddAsync(entity);
            await SendAsync(Map.FromEntity(entity));
        }
    }
}