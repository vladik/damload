using DamLoad.Assets.Api.V1.Folders.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Folders.Validators;

public class RenameFolderValidator : Validator<RenameFolderRequest>
{
    public RenameFolderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Folder ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("New name is required.")
            .MaximumLength(100);
    }
}