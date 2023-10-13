using SjxPay.Core.Exceptions;
using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Request
{
    public class OAuthRequest:BaseRequest<OAuthModel,OAuthResponse>
    {
        public OAuthRequest()
        {
            RequestUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
        }
        internal override void Execute(Merchant merchant)
        {
            if (string.IsNullOrEmpty(merchant.AppSecret))
            {
                throw new GatewayException("请设置AppSecret");
            }

            GatewayData.Add("secret", merchant.AppSecret);
            GatewayData.Remove("notify_url");
            GatewayData.Remove("sign_type");
            GatewayData.Remove("mch_id");
        }
    }
}
