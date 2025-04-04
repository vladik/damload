using DamLoad.Assets.Api.V1.Tags.Mappers;
using DamLoad.Assets.Api.V1.Tags.Requests;
using DamLoad.Assets.Api.V1.Tags.Responses;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Tags.Endpoints
{
    public class GetTag : Endpoint<GetTagRequest, TagResponse, TagMapper>
    {
        private readonly ITagService _tagService;

        public GetTag(ITagService tagService) => _tagService = tagService;

        public override void Configure()
        {
            Get("/api/v1/tags/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetTagRequest req, CancellationToken ct)
        {
            var tag = await _tagService.GetByIdAsync(req.Id);
            if (tag is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendAsync(Map.FromEntity(tag), cancellation: ct);
        }
    }
}
