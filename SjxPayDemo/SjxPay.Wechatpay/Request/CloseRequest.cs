using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Request
{
    public class CloseRequest : BaseRequest<CloseModel, CloseResponse>
    {
        public CloseRequest()
        {
            RequestUrl = "/pay/closeorder";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
