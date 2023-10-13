using SjxPay.Core.Attributes;
using SjxPay.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Response
{
    public class PublicKeyResponse : BaseResponse
    {
        /// <summary>
        /// RSA 公钥
        /// </summary>
        [ReName(Name = "pub_key")]
        public string PublicKey { get; set; }

        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
        }
    }
}
