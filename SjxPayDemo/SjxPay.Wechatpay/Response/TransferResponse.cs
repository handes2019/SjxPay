using SjxPay.Core.Attributes;
using SjxPay.Core.Geteways;
using SjxPay.Core.Request;
using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Response
{
    public class TransferResponse : BaseResponse
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [ReName(Name = "mch_appid")]
        public new string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [ReName(Name = "mchid")]
        public new string MchId { get; set; }

        /// <summary>
        /// 微信企业付款单号
        /// </summary>
        [ReName(Name = "payment_no")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [ReName(Name = "partner_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 微信支付成功时间
        /// </summary>
        [ReName(Name = "payment_time")]
        public DateTime PayTime { get; set; }

        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
        }
    }
}
