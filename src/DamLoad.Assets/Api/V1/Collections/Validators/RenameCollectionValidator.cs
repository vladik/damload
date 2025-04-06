using DamLoad.Assets.Api.V1.Collections.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Collections.Validators
{
    public class RenameCollectionValidator : Validator<RenameCollectionRequest>
    {
        public RenameCollectionValidator()
        {
            RuleFor(x => x.NewName)
                .NotEmpty().WithMessage("New name is required.")
                .MaximumLength(128);
        }
    }

}
