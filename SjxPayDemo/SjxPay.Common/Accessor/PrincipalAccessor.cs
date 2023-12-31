﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Common.Accessor
{
    public class PrincipalAccessor : IPrincipalAccessor
    {
        //没有通过认证的，User会为空
        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }

    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}
