using DamLoad.Classify.Api.V1.Classifiers.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Classify.Api.V1.Classifiers.Validators
{
    public class UpdateClassifierValidator : Validator<UpdateClassifierRequest>
    {
        public UpdateClassifierValidator()
        {
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(100);
        }
    }
}