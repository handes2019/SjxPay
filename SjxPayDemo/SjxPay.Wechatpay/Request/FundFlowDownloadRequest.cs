using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Request
{
    public class FundFlowDownloadRequest:BaseRequest<FundFlowDownloadModel,FundFlowDownloadResponse>
    {
        public FundFlowDownloadRequest()
        {
            RequestUrl = "/pay/downloadfundflow";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
            GatewayData.Add("sign_type", "HMAC-SHA256");
        }
    }
}
