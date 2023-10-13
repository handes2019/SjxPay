using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Request
{
    public class RefundQueryRequest : BaseRequest<RefundQueryModel, RefundQueryResponse>
    {
        public RefundQueryRequest()
        {
            RequestUrl = "/pay/refundquery";
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
