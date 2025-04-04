using FluentValidation;
using DamLoad.Assets.Api.V1.Assets.Requests;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Validators;

public class ListAssetsValidator : Validator<ListAssetsRequest>
{
    public ListAssetsValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}