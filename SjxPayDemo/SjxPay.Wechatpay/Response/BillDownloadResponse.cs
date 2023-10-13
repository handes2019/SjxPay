using SjxPay.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SjxPay.Wechatpay.Response
{
    public class BillDownloadResponse:BaseResponse
    {
        private byte[] _billFile;
        /// <summary>
        /// 获取账单文件
        /// </summary>
        public byte[] GetBillFile()
        {
            return _billFile;
        }
        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
            if (!string.IsNullOrEmpty(Raw))
            {
                _billFile = Encoding.UTF8.GetBytes(Raw);
            }
        }
    }
}
