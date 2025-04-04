using DamLoad.Assets.Api.V1.Tags.Requests;
using DamLoad.Assets.Api.V1.Tags.Responses;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Tags.Endpoints;

public class RenameTag : Endpoint<RenameTagRequest, RenameTagResponse>
{
    private readonly ITagService _tagService;

    public RenameTag(ITagService tagService)
    {
        _tagService = tagService;
    }

    public override void Configure()
    {
        Put("/api/v1/tags/{tagId:guid}/rename");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RenameTagRequest req, CancellationToken ct)
    {
        await _tagService.RenameAsync(req.TagId, req.Name);

        await SendAsync(new RenameTagResponse
        {
            TagId = req.TagId,
            NewName = req.Name
        }, cancellation: ct);
    }
}
