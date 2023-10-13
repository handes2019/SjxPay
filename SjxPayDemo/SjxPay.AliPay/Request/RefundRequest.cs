using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Response;

namespace SjxPay.AliPay.Request
{
    public class RefundRequest : BaseRequest<RefundModel, RefundResponse>
    {
        public RefundRequest()
            : base("alipay.trade.refund")
        {
        }
    }
}
