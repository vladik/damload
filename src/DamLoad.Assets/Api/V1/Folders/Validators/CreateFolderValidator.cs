using DamLoad.Assets.Api.V1.Folders.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Folders.Validators;

public class CreateFolderValidator : Validator<CreateFolderRequest>
{
    public CreateFolderValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Folder name is required.")
            .MaximumLength(100);

        RuleFor(x => x.SortOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Sort order must be 0 or greater.");
    }
}