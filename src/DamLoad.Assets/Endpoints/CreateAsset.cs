using DamLoad.Abstractions.Events;
using DamLoad.Abstractions.Models;
using DamLoad.Abstractions.Workflow;
using DamLoad.Assets.Entities;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.Endpoints
{
    public class CreateAsset : EndpointWithoutRequest
    {
        private readonly IWorkflowStatusProvider<AssetEntity> _workflowProvider;
        private readonly Abstractions.Events.IEventBus _eventBus;

        public CreateAsset(
            IWorkflowStatusProvider<AssetEntity> workflowProvider,
            Abstractions.Events.IEventBus eventBus)
        {

            _workflowProvider = workflowProvider ?? throw new ArgumentNullException(nameof(workflowProvider));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public override void Configure()
        {
            Post("/api/assets/test");
            AllowAnonymous(); // Or use authentication/authorization as needed
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var status = _workflowProvider.GetDefaultStatus();
            await _eventBus.PublishAsync(new EntityEvent<AssetModel>
            {
                Identifier = "damload.assets:created",
                Data = new AssetModel { Status = status }
            });

            await SendOkAsync(ct);
        }
    }
}
