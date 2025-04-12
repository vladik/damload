using DamLoad.Classify.Api.V1.Schemes.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Classify.Api.V1.Schemes.Validators
{
    public class CreateSchemeValidator : Validator<CreateSchemeRequest>
    {
        public CreateSchemeValidator()
        {
            RuleFor(x => x.Slug).NotEmpty().WithMessage("Slug must be provided to uniquely identify the scheme.").MaximumLength(100);
            RuleFor(x => x.Label).MaximumLength(255);
            RuleFor(x => x.Properties).NotNull();
        }
    }
}
