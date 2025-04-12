using DamLoad.Assets.Api.V1.Folders.Mappers;
using DamLoad.Assets.Api.V1.Folders.Requests;
using DamLoad.Assets.Api.V1.Folders.Responses;
using DamLoad.Assets.Api.V1.Folders.Validators;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Folders.Endpoints;

public class RenameFolder : Endpoint<RenameFolderRequest, RenameFolderResponse, RenameFolderMapper>
{
    private readonly IFolderService _folderService;

    public RenameFolder(IFolderService folderService) => _folderService = folderService;

    public override void Configure()
    {
        Put("/api/v1/folders/{folderId:guid}/rename");
        AllowAnonymous();
        Validator<RenameFolderValidator>();
    }

    public override async Task HandleAsync(RenameFolderRequest req, CancellationToken ct)
    {
        var folderId = Route<Guid>("folderId");
        await _folderService.RenameFolderAsync(folderId, req.Name);

        var folder = await _folderService.GetFolderByIdAsync(folderId);
        if (folder is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(Map.FromEntity(folder), cancellation: ct);
    }
}
