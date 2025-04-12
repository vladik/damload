using Microsoft.AspNetCore.Mvc;

namespace DamLoad.Classify.Api.V1.Schemes.Requests
{
    public class GetSchemeRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }
}
