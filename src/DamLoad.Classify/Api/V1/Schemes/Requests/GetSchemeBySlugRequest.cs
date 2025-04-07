using Microsoft.AspNetCore.Mvc;

namespace DamLoad.Classify.Api.V1.Schemes.Requests
{
    public class GetSchemeBySlugRequest
    {
        [FromRoute]
        public string Slug { get; set; } = string.Empty;
    }
}
