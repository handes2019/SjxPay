﻿using Microsoft.Extensions.Options;
using SjxPay.AliPay.Request;
using SjxPay.AliPay.Response;
using SjxPay.Core.Exceptions;
using SjxPay.Core.Geteways;
using SjxPay.Core.Request;
using SjxPay.Core.Utils;


namespace SjxPay.AliPay
{
    public sealed class AlipayGateway:BaseGateway
    {
        #region 私有字段
        private readonly Merchant _merchant;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化支付宝网关
        /// </summary>
        /// <param name="merchant"></param>
        public AlipayGateway(Merchant merchant):base(merchant)
        {
            _merchant = merchant;
        }
        public AlipayGateway(IOptions<Merchant> merchant):this(merchant.Value) { }
        #endregion
        #region 属性
        public override string GatewayUrl { get; set; } = "https://openapi.alipay.com";

        public new NotifyResponse NotifyResponse => (NotifyResponse)base.NotifyResponse;

        protected override bool IsPaySuccess => NotifyResponse.TradeStatus == "TRADE_SUCCESS" && !IsRefundSuccess;

        protected override bool IsRefundSuccess => NotifyResponse.RefundAmount > 0;

        protected override bool IsCancelSuccess { get; }

        protected override string[] NotifyVerifyParameter => new string[]
        {
            "app_id","version", "charset","trade_no", "sign","sign_type"
        };
        #endregion
        #region 公共方法

        protected override async Task<bool> ValidateNotifyAsync()
        {
            base.NotifyResponse = await GatewayData.ToObjectAsync<NotifyResponse>(StringCase.Snake);
            base.NotifyResponse.Raw = GatewayData.ToUrl(false);
            GatewayData.Remove("sign");
            GatewayData.Remove("sign_type");

            var result = EncryptUtil.RSAVerifyData(GatewayData.ToUrl(false),
                NotifyResponse.Sign, _merchant.AlipayPublicKey, _merchant.SignType);
            if (result)
            {
                return true;
            }

            throw new GatewayException("签名不一致");
        }

        public override TResponse Execute<TModel, TResponse>(Request<TModel, TResponse> request)
        {
            if (request is WapPayRequest || request is WebPayRequest || request is AppPayRequest)
            {
                return SubmitProcess.SdkExecute(_merchant, request, GatewayUrl);
            }

            return SubmitProcess.Execute(_merchant, request, GatewayUrl);
        }

        #endregion
    }
}
