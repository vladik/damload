using DamLoad.Classify.Api.V1.Classifications.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Classify.Api.V1.Classifications.Validators;

public class DeleteClassificationValidator : Validator<DeleteClassificationRequest>
{
    public DeleteClassificationValidator()
    {
        RuleFor(x => x.ResourceId).NotEmpty();
        RuleFor(x => x.ClassifierIds).NotEmpty();
    }
}