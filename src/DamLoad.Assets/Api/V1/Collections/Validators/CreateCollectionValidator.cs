using DamLoad.Assets.Api.V1.Collections.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Collections.Validators
{
    public class CreateCollectionValidator : Validator<CreateCollectionRequest>
    {
        public CreateCollectionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Collection name is required.")
                .MaximumLength(128);
        }
    }

}
