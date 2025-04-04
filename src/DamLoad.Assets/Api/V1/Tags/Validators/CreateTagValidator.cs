using DamLoad.Assets.Api.V1.Tags.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Tags.Validators
{
    public class CreateTagValidator : Validator<CreateTagRequest>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}
