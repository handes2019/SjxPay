using Microsoft.Extensions.Configuration;
using SjxPay.Core.Geteways;
using System;

namespace SjxPay.Wechatpay
{
    public static class ServiceCollectionExtensions
    {
        public static IGateways UseWechatpay(this IGateways gateways,Action<Merchant> action)
        {
            if(action != null)
            {
                var merchat = new Merchant();
                action(merchat);
                gateways.Add(new WechatpayGateway(merchat));
            }
            return gateways;
        }

        public static IGateways UseWechatpay(this IGateways gateways,IConfiguration configuration)
        {
            var merchants = configuration.GetSection("SjxPay:Wechatpays").Get<Merchant[]>();
            if (merchants != null)
            {
                for(var i = 0;i<merchants.Length;i++) 
                {
                    var wechatpayGateway = new WechatpayGateway(merchants[i]);
                    var gatewayUrl = configuration.GetSection($"SjxPay:Wechatpays:{i}:GatewayUrl").Value;
                    if (!string.IsNullOrEmpty(gatewayUrl))
                    {
                        wechatpayGateway.GatewayUrl = gatewayUrl;
                    }

                    gateways.Add(wechatpayGateway);
                }
            }
            return gateways;
        }
    }
}
