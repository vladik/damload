using DamLoad.Classify.Api.V1.Classifiers.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Classify.Api.V1.Classifiers.Validators
{
    public class CreateClassifierValidator : Validator<CreateClassifierRequest>
    {
        public CreateClassifierValidator()
        {
            RuleFor(x => x.SchemeId).NotEmpty();
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(100);
        }
    }
}