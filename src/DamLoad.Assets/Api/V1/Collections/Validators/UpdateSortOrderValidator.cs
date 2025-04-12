using DamLoad.Assets.Api.V1.Collections.Requests;
using FastEndpoints;
using FluentValidation;

namespace DamLoad.Assets.Api.V1.Collections.Validators
{
    public class UpdateSortOrderValidator : Validator<UpdateSortOrderRequest>
    {
        public UpdateSortOrderValidator()
        {
            RuleFor(x => x.NewSortOrder).GreaterThanOrEqualTo(0);
        }
    }

}
