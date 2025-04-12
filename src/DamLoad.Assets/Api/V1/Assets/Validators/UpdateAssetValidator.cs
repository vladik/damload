using DamLoad.Assets.Api.V1.Assets.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Assets.Validators;

public class UpdateAssetValidator : Validator<UpdateAssetRequest>
{
    public UpdateAssetValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
