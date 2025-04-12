using FastEndpoints;
using DamLoad.Assets.Services;

namespace DamLoad.Assets.Api.V1.Folders.Endpoints;

public class DeleteFolder : EndpointWithoutRequest
{
    private readonly IFolderService _folderService;

    public DeleteFolder(IFolderService folderService) => _folderService = folderService;

    public override void Configure()
    {
        Delete("/api/v1/folders/{folderId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var folderId = Route<Guid>("folderId");
        await _folderService.DeleteFolderAsync(folderId);
        await SendOkAsync(cancellation: ct);
    }
}
