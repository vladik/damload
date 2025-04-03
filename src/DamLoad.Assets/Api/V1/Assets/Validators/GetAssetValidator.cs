using DamLoad.Assets.Api.V1.Assets.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.Assets.Validators
{
    public class GetAssetValidator : Validator<GetAssetRequest>
    {
        public GetAssetValidator() {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
