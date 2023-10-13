using Microsoft.Extensions.Options;
using SjxPay.Core.Exceptions;
using SjxPay.Core.Geteways;
using SjxPay.Core.Request;
using SjxPay.Core.Utils;
using SjxPay.Wechatpay.Request;
using SjxPay.Wechatpay.Response;
using System;
using System.Threading.Tasks;
using static SjxPay.Wechatpay.Response.QueryResponse;

namespace SjxPay.Wechatpay
{
    public class WechatpayGateway:BaseGateway
    {
        #region 私有字段
        private readonly Merchant _merchant;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化微信支付网关
        /// </summary>
        /// <param name="merchant">商户数据</param>
        public WechatpayGateway(Merchant merchant):base(merchant)
        {
            _merchant = merchant;
        }
        public WechatpayGateway(IOptions<Merchant> merchant):this(merchant.Value)
        {
        }
        #endregion

        #region 属性
        public override string GatewayUrl { get; set; } = "https://api.mch.weixin.qq.com";
        public new Merchant Merchant => _merchant;
        public new NotifyResponse NotifyResponse => (NotifyResponse)base.NotifyResponse;
        protected override bool IsPaySuccess => NotifyResponse.ResultCode == "SUCCESS" && !string.IsNullOrEmpty(NotifyResponse.TradeType);
        protected override bool IsRefundSuccess => NotifyResponse.RefundStatus == "SUCCESS";
        protected override bool IsCancelSuccess { get; }
        protected override string[] NotifyVerifyParameter => new string[] 
        {
            "appid","return_code","mch_id","nonce_str"
        };
        #endregion
        #region 方法
        protected override async Task<bool> ValidateNotifyAsync()
        {
            base.NotifyResponse = await GatewayData.ToObjectAsync<NotifyResponse>(Core.Utils.StringCase.Snake);
            base.NotifyResponse.Raw = GatewayData.Raw;
            if (NotifyResponse.ResultCode != "SUCCESS")
            {
                throw new GatewayException("不是成功的返回码");
            }
            if(string.IsNullOrEmpty(NotifyResponse.ReqInfo))
            {
                NotifyResponse.Coupons = ConvertUtil.ToList<CouponResponse, object>(GatewayData, -1);
                if (NotifyResponse.Sign != SubmitProcess.BuildSign(GatewayData, _merchant.Key))
                {
                    throw new GatewayException("签名不一致");
                }
            }
            else
            {
                var tempNotify = NotifyResponse;
                var key = EncryptUtil.MD5(_merchant.Key).ToLower();
                var data = EncryptUtil.AESDecrypt(NotifyResponse.ReqInfo, key);
                var gatewayData = new GatewayData();
                base.NotifyResponse = await gatewayData.ToObjectAsync<NotifyResponse>(StringCase.Snake);
                GatewayData.Add(NotifyResponse, StringCase.Snake);
                NotifyResponse.AppId = tempNotify.AppId;
                NotifyResponse.MchId = tempNotify.MchId;
                NotifyResponse.NonceStr = tempNotify.NonceStr;
                NotifyResponse.ReqInfo= tempNotify.ReqInfo; 
                NotifyResponse.ReturnCode = tempNotify.ReturnCode;
            }
            return true;
        }
        protected override void WriteSuccessFlag()
        {
            GatewayData.Clear();
            GatewayData.Add("return_code", "SUCCESS");
            GatewayData.Add("return_msg", "OK");
            HttpUtil.Write(GatewayData.ToXml());
        }
        protected override void WriteFailureFlag()
        {
            GatewayData.Clear();
            GatewayData.Add("return_code", "FAIL");
            HttpUtil.Write(GatewayData.ToXml());
        }
        public override TResponse Execute<TModel, TResponse>(Request<TModel, TResponse> request)
        {
            if(request is OAuthRequest)
            {
                return SubmitProcess.AuthExecute(_merchant, request, GatewayUrl);
            }
            return SubmitProcess.Execute(_merchant, request, GatewayUrl);
        }
        #endregion
    }
}
