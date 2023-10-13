using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Response;


namespace SjxPay.AliPay.Request
{
    public class WebPayRequest : BaseRequest<WebPayModel, WebPayResponse>
    {
        public WebPayRequest()
            : base("alipay.trade.page.pay")
        {

        }
    }
}