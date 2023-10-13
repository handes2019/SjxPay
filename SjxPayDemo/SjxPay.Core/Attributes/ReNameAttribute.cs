using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Core.Attributes
{
    /// <summary>
    /// 重命名属性
    /// </summary>
    public class ReNameAttribute:Attribute
    {
        public string Name { get; set; }
    }
}
