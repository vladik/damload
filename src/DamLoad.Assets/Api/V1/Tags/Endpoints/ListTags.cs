using DamLoad.Assets.Api.V1.Tags.Responses;
using DamLoad.Assets.Repositories;
using DamLoad.Assets.Services;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Tags.Endpoints
{
    public class ListTags : EndpointWithoutRequest<List<TagResponse>>
    {
        private readonly ITagService _tagService;

        public ListTags(ITagService tagService) => _tagService = tagService;

        public override void Configure()
        {
            Get("/api/v1/tags");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var tags = await _tagService.GetAllAsync();
            var response = tags.Select(t => new TagResponse { Id = t.Id, Name = t.Name }).ToList();
            await SendAsync(response, cancellation: ct);
        }
    }
}
