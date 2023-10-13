using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Request
{
    public class TransferQueryRequest : BaseRequest<TransferQueryModel, TransferQueryResponse>
    {
        public TransferQueryRequest()
        {
            RequestUrl = "/mmpaymkttransfers/gettransferinfo";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
            GatewayData.Remove("sign_type");
        }
    }
}
