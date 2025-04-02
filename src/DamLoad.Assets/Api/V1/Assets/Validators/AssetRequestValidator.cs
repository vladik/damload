using DamLoad.Assets.Api.Assets.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.Assets.Validators
{
    public class AssetRequestValidator : Validator<AssetRequest>
    {
        public AssetRequestValidator() {
            RuleFor(x => x.Inp)
                .NotEmpty();
        }
    }
}
