using DamLoad.Assets.Api.V1.Folders.Responses;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Folders.Endpoints;

public class ListFolders : EndpointWithoutRequest<List<FolderResponse>>
{
    private readonly IFolderService _folderService;

    public ListFolders(IFolderService folderService) => _folderService = folderService;

    public override void Configure()
    {
        Get("/api/v1/folders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var folders = await _folderService.GetAllAsync();
        var response = folders.Select(f => new FolderResponse
        {
            Id = f.Id,
            Name = f.Name,
            ParentId = f.ParentId,
            SortOrder = f.SortOrder
        }).ToList();

        await SendAsync(response, cancellation: ct);
    }
}
