using SjxPay.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Response
{
    public class CancelResponse : BaseResponse
    {
        /// <summary>
        /// 是否重调
        /// </summary>
        public string Recall { get; set; }

        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
        }
    }
}
