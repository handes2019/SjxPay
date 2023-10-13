using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Response;

namespace SjxPay.AliPay.Request
{
    public class WapPayRequest : BaseRequest<WapPayModel, WapPayResponse>
    {
        public WapPayRequest()
            : base("alipay.trade.wap.pay")
        {
        }
    }
}
