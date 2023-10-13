using SjxPay.AliPay.Request;
using SjxPay.Core.Response;


namespace SjxPay.AliPay.Response
{
    public class AppPayResponse : IResponse
    {
        public AppPayResponse(AppPayRequest request)
        {
            OrderInfo = request.GatewayData.ToUrl();
        }

        /// <summary>
        /// 用于唤起App的订单参数
        /// </summary>
        public string OrderInfo { get; set; }

        public string Raw { get; set; }
    }
}
