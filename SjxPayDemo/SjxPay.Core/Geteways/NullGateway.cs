using SjxPay.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Core.Geteways
{
    /// <summary>
    /// 未知网关
    /// </summary>
    public class NullGateway : BaseGateway
    {
        public override string GatewayUrl { get; set; }

        protected internal override bool IsPaySuccess { get; }

        protected internal override bool IsRefundSuccess { get; }

        protected internal override bool IsCancelSuccess { get; }

        protected internal override string[] NotifyVerifyParameter { get; }

        protected internal override async Task<bool> ValidateNotifyAsync()
        {
            return await Task.Run(() => { return false; });
        }

        public override TResponse Execute<TModel, TResponse>(Request<TModel, TResponse> request)
        {
            throw new NotImplementedException();
        }
    }
}
