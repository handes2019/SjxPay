using SjxPay.Core.Request;
using SjxPay.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPay.Core.Geteways
{
    public interface IGateway
    {
        TResponse Execute<TModel, TResponse>(Request<TModel,TResponse> request) where TResponse:IResponse;
    }
}
