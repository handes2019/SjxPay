using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Entities;
using SjxPay.Common;
using SjxPay.Common.Accessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SjxPay.Domain
{
    /// <summary>
    /// 无指定实体的仓储基类
    /// </summary>
    public abstract class BaseManager : IScopedDependency
    {
        public ILog? Logger { get; set; }
        public IConfiguration Configuration { get; set; }
        public IClaimsAccessor UserClaims { get; set; }

        public BaseManager()
        {
            UserClaims = ServiceProviderInstance.Instance.GetRequiredService<IClaimsAccessor>();
            Logger = (ILog?)ServiceProviderInstance.Instance.GetService(typeof(ILog));
            Configuration = (IConfiguration?)ServiceProviderInstance.Instance.GetService(typeof(IConfiguration));
        }
    }
    /// <summary>
    /// 数据连接层代理基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseManager<T> : BaseManager where T : class, IEntity, new()
    {
        /// <summary>
        /// 修改或新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <param name="modifyExcept"></param>
        /// <returns></returns>
        public virtual async Task<long> UpsertAsync(T entity, Expression<Func<T, bool>> expression, Expression<Func<T, object>> modifyScope = null, bool isExcept = false)
        {
            var dbent = DB.Find<T>().Match(expression).ExecuteAsync().Result;
            if (dbent.Any())
            {
                var updateResult = await this.UpdateAsync(entity, expression, modifyScope, isExcept);
                return updateResult.ModifiedCount;
            }
            else
            {
                return await InsertAsync(entity);
            }
        }
        public virtual async Task<UpdateResult> UpdateAsync(T entity, Expression<Func<T, bool>> expression, Expression<Func<T, object>> modifyScope = null, bool isExcept = false)
        {
            var update = DB.Update<T>().Match(expression);
            if (modifyScope == null)
            {
                update = update.ModifyWith(entity);
            }
            else if (isExcept)
            {
                update = update.ModifyExcept(modifyScope, entity);
            }
            else
            {
                update = update.ModifyOnly(modifyScope, entity);
            }
            var updateResult = await update.ExecuteAsync();
            Logger?.Debug($"更新成功：匹配{updateResult.MatchedCount}条数据，更新{updateResult.ModifiedCount}条数据！");

            return updateResult;
        }
        public virtual async Task<long> InsertAsync(T entity, CancellationToken cancellation = default)
        {
            await DB.InsertAsync(entity, null, cancellation);
            Logger?.Debug($"新增成功！");
            return 1;
        }
        public virtual Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation = default)
            => DB.DeleteAsync<T>(expression, null, cancellation);
        public Task<List<T>> MatchAsync(Expression<Func<T, bool>> expression) => DB.Find<T>().Match(expression).ExecuteAsync();
        public Task<T?> GetById(string id) => DB.Find<T>().OneAsync(id);
        public Task<List<T>> GetAll() => MatchAsync(o => true);
        public virtual T CreateNew() => DB.Entity<T>();

        public IAggregateFluent<T> Fluent()
        {
            return DB.Fluent<T>();
        }
        public virtual async Task<bool> IsExists(Expression<Func<T, bool>> expression)
        {
            return await DB.Find<T>().Match(expression).ExecuteAnyAsync();
        }
        public virtual async Task<(IReadOnlyList<T> Results, long TotalCount, int PageCount)> GetPage(Expression<Func<T, bool>> expression, int pageNum, int pageSize, Func<SortDefinitionBuilder<T>, SortDefinition<T>>? sortFunction = null)
        {
            var result = await DB.PagedSearch<T>()
                 .Match(expression)
                 .If(sortFunction != null, page => page.Sort(sortFunction))
                 .Sort(q => q.ID, Order.Ascending)
                 .PageNumber(pageNum)
                 .PageSize(pageSize)
                 .ExecuteAsync();
            return result;
        }
        public virtual async Task<(IReadOnlyList<T> Results, long TotalCount, int PageCount)> GetPage(Expression<Func<T, bool>> expression, IPageRequest r, Func<SortDefinitionBuilder<T>, SortDefinition<T>>? sortFunction = null)
        {
            var result = await DB.PagedSearch<T>()
                 .Match(expression)
                 .If(sortFunction != null, page => page.Sort(sortFunction))
                 .Sort(q => q.ID, Order.Ascending)
                 .PageNumber(r.PageIndex.Value)
                 .PageSize(r.PageSize.Value)
                 .ExecuteAsync();
            return result;
        }
    }

    public abstract class BaseAuditManager<T> : BaseManager<T> where T : AuditEntity, new()
    {
        protected DBContext Context { get; set; }
        public BaseAuditManager() : base()
        {
            Context = new DBContext(new() { UserID = UserClaims.UserId.ToString(), UserName = UserClaims.UserName });
        }
        public BaseAuditManager(DBContext context)
        {
            Context = context;
        }
        public override Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation = default)
        {
            return Context.DeleteAsync(expression, cancellation);
        }
        public override async Task<UpdateResult> UpdateAsync(T entity, Expression<Func<T, bool>> expression, Expression<Func<T, object>> modifyScope = null, bool isExcept = false)
        {
            var update = Context.Update<T>().Match(expression);
            if (modifyScope == null)
            {
                update = update.ModifyExcept(o => new { o.CreatedOn, o.CreateUserId, o.CreatorHeader, o.CreatorName }, entity);
            }
            else if (isExcept)
            {
                update = update.ModifyExcept(modifyScope, entity);
            }
            else
            {
                update = update.ModifyOnly(modifyScope, entity);
            }
            var updateResult = await update.ExecuteAsync();
            Logger?.Debug($"更新成功：匹配{updateResult.MatchedCount}条数据，更新{updateResult.ModifiedCount}条数据！");

            return updateResult;
        }
        public override async Task<long> InsertAsync(T entity, CancellationToken cancellation = default)
        {
            entity.CreateUserId = Context.ModifiedBy?.UserID;
            entity.CreatorHeader = UserClaims.UserHead;
            entity.CreatorName = UserClaims.UserName;
            await Context.InsertAsync(entity);
            return 1;
        }
        public override T CreateNew()
        {
            var res = base.CreateNew();
            res.CreateUserId = Context.ModifiedBy?.UserID;
            res.CreatorHeader = UserClaims?.UserHead;
            res.CreatorName = UserClaims?.UserName;
            return res;
        }
    }
}
