using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Response;

namespace SjxPay.AliPay.Request
{
    public class RefundQueryRequest : BaseRequest<RefundQueryModel, RefundQueryResponse>
    {
        public RefundQueryRequest()
            : base("alipay.trade.fastpay.refund.query")
        {
        }
    }
}
