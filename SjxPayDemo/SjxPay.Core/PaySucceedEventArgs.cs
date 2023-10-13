﻿using SjxPay.Core.Events;
using SjxPay.Core.Geteways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Core
{
    /// <summary>
    /// 支付成功网关事件数据
    /// </summary>
    public class PaySucceedEventArgs : NotifyEventArgs
    {
        #region 构造函数

        /// <summary>
        /// 初始化支付成功网关事件数据
        /// </summary>
        /// <param name="gateway">支付网关</param>
        public PaySucceedEventArgs(BaseGateway gateway)
            : base(gateway)
        {
        }

        #endregion
    }
}
