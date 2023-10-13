using FluentValidation;
using SjxPay.API.Base;

namespace SjxPay.API.NotifyAPI
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
               
            });
        }
    }
}
