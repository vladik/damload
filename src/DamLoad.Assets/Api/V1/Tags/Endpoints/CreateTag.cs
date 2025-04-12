using DamLoad.Assets.Api.V1.Tags.Mappers;
using DamLoad.Assets.Api.V1.Tags.Requests;
using DamLoad.Assets.Api.V1.Tags.Responses;
using DamLoad.Assets.Api.V1.Tags.Validators;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Tags.Endpoints;

public class CreateTag : Endpoint<CreateTagRequest, TagResponse, TagMapper>
{
    private readonly ITagService _tagService;

    public CreateTag(ITagService tagService) => _tagService = tagService;

    public override void Configure()
    {
        Post("/api/v1/tags");
        AllowAnonymous();
        Validator<CreateTagValidator>();
    }

    public override async Task HandleAsync(CreateTagRequest req, CancellationToken ct)
    {
        var tag = Map.ToEntity(req);
        await _tagService.AddAsync(tag);
        await SendAsync(Map.FromEntity(tag), cancellation: ct);
    }
}