using DamLoad.Assets.Api.V1.Assets.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Assets.Validators;

public class UploadAssetValidator : Validator<UploadAssetRequest>
{
    public UploadAssetValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(f => f.Length > 0).WithMessage("File cannot be empty.");
    }
}
