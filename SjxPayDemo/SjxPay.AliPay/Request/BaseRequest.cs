using SjxPay.Core.Geteways;
using SjxPay.Core.Request;
using SjxPay.Core.Response;
using SjxPay.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.AliPay.Request
{
    public class BaseRequest<TModel, TResponse> : Request<TModel, TResponse> where TResponse : IResponse
    {
        public BaseRequest(string method)
        {
            RequestUrl = "/gateway.do?charset=UTF-8";
            GatewayData.Add("method", method);
        }

        public override void AddGatewayData(TModel model)
        {
            base.AddGatewayData(model);

            GatewayData.Add("biz_content", Util.SerializeObject(model));
        }
    }
}
