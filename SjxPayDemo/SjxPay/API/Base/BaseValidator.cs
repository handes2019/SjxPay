using FluentValidation.Internal;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentValidation.Results;

namespace SjxPay.API.Base
{
    public class BaseValidator<TRequest> : Validator<TRequest> where TRequest : notnull, BaseRequest
    {
        public override Task<ValidationResult> ValidateAsync(FluentValidation.ValidationContext<TRequest> context, CancellationToken cancellation = default)
        {
            var instance = context.InstanceToValidate;
            context = new FluentValidation.ValidationContext<TRequest>(instance, new PropertyChain(),
                ValidatorOptions.Global.ValidatorSelectors.RulesetValidatorSelectorFactory(new string[] { instance.method.ToLower() }));
            return base.ValidateAsync(context, cancellation);
        }
        public BaseValidator()
        {
            RuleSet("delete", () => { RuleFor(o => o.ID).NotEmpty().WithMessage("参数[ID]不可为空"); });
        }
    }
}
