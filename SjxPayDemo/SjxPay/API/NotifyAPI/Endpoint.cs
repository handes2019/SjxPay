using SjxPay.API.Base;
using SjxPay.Core.Geteways;
using SjxPay.Core.Notify;
using SjxPay.Wechatpay;
using SjxPay.Wechatpay.Response;

namespace SjxPay.API.NotifyAPI
{
    public class Endpoint : BaseEndPoint<Request, Mapper>
    {
        private readonly IGateways _gateways;
        public Endpoint(IGateways gateways)
        {
            _gateways = gateways;
        }
        public override void Configure()
        {
            Business = "Notify";
            base.Configure();

        }
        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            switch (r.method)
            {
                case "Index": await Index(); break;
                default: await base.HandleAsync(r, c); break;
            }
        }
        protected async override Task TestEndPoint(Request r)
        {

        }

        private async Task Index()
        {
            // 订阅支付通知事件
            var notify = new Notify(_gateways);
            notify.PaySucceed += Notify_PaySucceed;
            notify.RefundSucceed += Notify_RefundSucceed;
            notify.CancelSucceed += Notify_CancelSucceed;
            notify.UnknownNotify += Notify_UnknownNotify;
            notify.UnknownGateway += Notify_UnknownGateway;
            // 接收并处理支付通知
            await notify.ReceivedAsync();
        }

        private void Notify_UnknownGateway(object arg1, Core.Events.UnknownGatewayEventArgs arg2)
        {
            if (arg2.GatewayType == typeof(Wechatpay.WechatpayGateway))
            {
                var alipayNotifyResponse = (NotifyResponse)arg2.NotifyResponse;

                //TODO
            }
            Logger.LogDebug(arg2.NotifyResponse.Raw);
            // 无法识别支付网关时的处理代码
        }

        private bool Notify_UnknownNotify(object arg1, Core.Events.UnKnownNotifyEventArgs arg2)
        {
            if (arg2.GatewayType == typeof(Wechatpay.WechatpayGateway))
            {
                var alipayNotifyResponse = (NotifyResponse)arg2.NotifyResponse;

                //TODO
            }
            Logger.LogDebug(arg2.NotifyResponse.Raw);
            // 未知时的处理代码
            return true;
        }

        private bool Notify_CancelSucceed(object arg1, Core.Events.CancelSucceedEventArgs arg2)
        {
            if (arg2.GatewayType == typeof(Wechatpay.WechatpayGateway))
            {
                var alipayNotifyResponse = (NotifyResponse)arg2.NotifyResponse;

                //TODO
            }
            Logger.LogDebug(arg2.NotifyResponse.Raw);
            // 订单撤销时的处理代码
            return true;
        }

        private bool Notify_RefundSucceed(object arg1, Core.Events.RefundSucceedEventArgs arg2)
        {
            if (arg2.GatewayType == typeof(Wechatpay.WechatpayGateway))
            {
                var alipayNotifyResponse = (NotifyResponse)arg2.NotifyResponse;

                //TODO
            }
            Logger.LogDebug(arg2.NotifyResponse.Raw);
            // 订单退款时的处理代码
            return true;
        }

        private bool Notify_PaySucceed(object arg1, Core.PaySucceedEventArgs arg2)
        {
            // 支付成功时时的处理代码
            /* 建议添加以下校验。
             * 1、需要验证该通知数据中的OutTradeNo是否为商户系统中创建的订单号，
             * 2、判断Amount是否确实为该订单的实际金额（即商户订单创建时的金额），
             */
            if (arg2.GatewayType == typeof(Wechatpay.WechatpayGateway))
            {
                var alipayNotifyResponse = (NotifyResponse)arg2.NotifyResponse;

                //同步通知，即浏览器跳转返回
                if (arg2.NotifyType == NotifyType.Sync)
                {
                    
                }
            }
            Logger.LogDebug(arg2.NotifyResponse.Raw);

            //处理成功返回true
            return true;
        }


    }
}
