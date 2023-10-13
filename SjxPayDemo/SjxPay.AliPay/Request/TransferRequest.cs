using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Response;

namespace SjxPay.AliPay.Request
{
    public class TransferRequest : BaseRequest<TransferModel, TransferResponse>
    {
        public TransferRequest()
            : base("alipay.fund.trans.toaccount.transfer")
        {
        }
    }
}
