using FastEndpoints;
using DamLoad.Assets.Api.V1.Folders.Requests;
using DamLoad.Assets.Api.V1.Folders.Responses;
using DamLoad.Assets.Api.V1.Folders.Mappers;
using DamLoad.Assets.Api.V1.Folders.Validators;
using DamLoad.Assets.Services;
using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Api.V1.Folders.Endpoints;

public class CreateFolder : Endpoint<CreateFolderRequest, FolderResponse, FolderMapper>
{
    private readonly IFolderService _folderService;

    public CreateFolder(IFolderService folderService) => _folderService = folderService;

    public override void Configure()
    {
        Post("/api/v1/folders");
        AllowAnonymous();
        Validator<CreateFolderValidator>();
    }

    public override async Task HandleAsync(CreateFolderRequest req, CancellationToken ct)
    {
        var folder = Map.ToEntity(req);
        await _folderService.AddFolderAsync(folder);
        await SendAsync(Map.FromEntity(folder), cancellation: ct);
    }
}