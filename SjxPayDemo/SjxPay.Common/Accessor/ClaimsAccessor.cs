using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Common.Accessor
{
    public class ClaimsAccessor : IClaimsAccessor
    {
        protected IPrincipalAccessor PrincipalAccessor { get; }

        public ClaimsAccessor(IPrincipalAccessor principalAccessor)
        {
            PrincipalAccessor = principalAccessor;
        }
        private const string name = "name";
        private const string sub = "sub";
        private const string nickname = "nickname";
        private const string avatar = "avatar";

        public string UserAccount { get => GetStringByClaims(name); }

        public string UserHead { get => GetStringByClaims(avatar); }

        public string UserName { get => GetStringByClaims(nickname); }

        public Guid UserId
        {
            get
            {
                var uId = GetStringByClaims(sub);
                if (!string.IsNullOrEmpty(uId))
                {
                    return Guid.Parse(uId);
                }
                return Guid.Empty;
            }
        }

        private string GetStringByClaims(string type)
        {
            var result = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == type)?.Value;
            if (string.IsNullOrWhiteSpace(result))
            {
                return string.Empty;
            }

            return result;
        }
    }

    public interface IClaimsAccessor
    {
        Guid UserId { get; }
        /// <summary>
        /// 登录用户账号
        /// </summary>
        string UserAccount { get; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 用户头像
        /// </summary>
        string UserHead { get; }
    }
}
