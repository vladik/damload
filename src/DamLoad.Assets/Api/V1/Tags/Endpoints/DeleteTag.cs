using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Tags.Endpoints;

public class DeleteTag : EndpointWithoutRequest
{
    private readonly ITagService _tagService;

    public DeleteTag(ITagService tagService)
    {
        _tagService = tagService;
    }

    public override void Configure()
    {
        Delete("/api/v1/tags/{tagId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var tagId = Route<Guid>("tagId");
        await _tagService.DeleteAsync(tagId);
        await SendOkAsync(ct);
    }
}
