namespace SjxPay.Common
{ 
    /// <summary>
    /// 分页参数接口
    /// </summary>
    public interface IPageRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
