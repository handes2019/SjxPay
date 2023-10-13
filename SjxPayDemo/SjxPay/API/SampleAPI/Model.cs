using FluentValidation;
using SjxPay.API.Base;

namespace SjxPay.API.SampleAPI
{
    public class Request : BaseRequest
    {
        public string Name { get; set; }
    }

    public class Validator : BaseValidator<Request>
    {
        public Validator()
        {
            RuleSet("GET", () =>
            {
                RuleFor(o => o.Name).NotEmpty().WithMessage("Name 不可为空");
            });
        }
    }
}
