﻿using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Request
{
    public class BillDownloadRequest : BaseRequest<BillDownloadModel, BillDownloadResponse> 
    {
        public BillDownloadRequest() 
        {
            RequestUrl = "/pay/downloadbill";
        }
        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
