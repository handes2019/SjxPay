using SjxPay.Common.Accessor;
using SjxPay.Common;
using System.Linq.Dynamic.Core;
using log4net;
using SjxPay.Core.Geteways;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SjxPay.API.Base
{
    public abstract class BaseEndPoint<TRequest, TMapper> : Endpoint<TRequest, CustomResponse, TMapper>
         where TRequest : notnull, BaseRequest
        where TMapper : notnull, IMapper
    {
        
       
        protected string? access_token { get; set; }
        public IClaimsAccessor UserAccessor { get; set; }
        protected string Business { get; set; }
        public async override void Configure()
        {
            Post("/pay/" + Business + "/{method}");
            AllowAnonymous();
            //AllowFormData(true);
        }
        public async override void OnBeforeHandle(TRequest req)
        {
            base.OnBeforeHandle(req);
            access_token = await HttpContext.GetTokenAsync("access_token");
        }
        public override async Task HandleAsync(TRequest r, CancellationToken c)
        {
            switch (r.method)
            {
                case "test": await TestEndPoint(r); break;
                default: ThrowError("无效路径，请求失败"); break;
            }
        }

        protected virtual Task TestEndPoint(TRequest r)
        {
            throw new NotImplementedException();
        }
        public async Task Success<U>(U item)
        {
            await SendAsync(CustomResponse.Success(item));
        }
        public async Task Success(string message)
        {
            await SendAsync(CustomResponse.Success(message));
        }
        public async Task ServerError(string message)
        {
            await SendAsync(CustomResponse.ServerError(message));
        }
        public async Task PageSuccess<U>(U res, long totalCount, int pageCount)
        {
            await SendAsync(CustomResponse.Success(new { Results = res, totalCount, pageCount }));
        }
        public async Task PageSuccess<U>(PagedResult<U> result)
        {
            await SendAsync(CustomResponse.Success(new { Results = result.Queryable.ToList(), TotalCount = result.RowCount, PageCount = result.PageCount }));
        }
    }

    public abstract class BaseRequest
    {
        public string ID { get; set; }
        [FromRoute]
        public string method { get; set; }
    }

    public abstract class PageRequest : BaseRequest, IPageRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
