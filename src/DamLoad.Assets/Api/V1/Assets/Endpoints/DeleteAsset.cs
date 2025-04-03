using DamLoad.Assets.Repositories;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Endpoints;

public class DeleteAsset : EndpointWithoutRequest
{
    private readonly IAssetRepository _repository;

    public DeleteAsset(IAssetRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Delete("/api/v1/assets/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var existing = await _repository.GetByIdAsync(id);

        if (existing is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _repository.DeleteAsync(id);

        await SendNoContentAsync(ct);
    }
}
