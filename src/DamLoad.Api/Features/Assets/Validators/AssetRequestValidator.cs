using DamLoad.Api.Features.Assets.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Api.Features.Assets.Validators
{
    public class AssetRequestValidator : Validator<AssetRequest>
    {
        public AssetRequestValidator() {
            RuleFor(x => x.Inp)
                .NotEmpty();
        }
    }
}
