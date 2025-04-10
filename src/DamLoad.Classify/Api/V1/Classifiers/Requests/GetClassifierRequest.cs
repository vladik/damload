using Microsoft.AspNetCore.Mvc;

namespace DamLoad.Classify.Api.V1.Classifiers.Requests
{
    public class GetClassifierRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }
}
