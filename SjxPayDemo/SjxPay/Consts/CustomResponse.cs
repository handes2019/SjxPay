namespace SjxPay.Consts
{
    public class CustomResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public static CustomResponse ServerError(string message)
        {
            return new CustomResponse
            {
                StatusCode = 500,
                Message = message
            };
        }
        public static CustomResponse Success(string message)
        {
            return new CustomResponse
            {
                StatusCode = 200,
                Message = message
            };
        }
        public static CustomResponse<T> Success<T>(T data, string message = "操作成功") where T : notnull
        {
            return new CustomResponse<T> { Datas = data, Message = message, StatusCode = 200 };
        }
        public static CustomListResponse<T> Success<T>(List<T> data, string message = "操作成功") where T : notnull, new()
        {
            return new CustomListResponse<T> { Datas = data, Message = message, StatusCode = 200 };
        }
        public static CustomResponse SuccessPage<T>(List<T> data, long totalCount, long pageCount) where T : notnull, new()
        {
            return Success(new { Results = data, TotalCount = totalCount, PageCount = pageCount });
        }
    }
    public class CustomResponse<T> : CustomResponse where T : notnull
    {
        public T Datas { get; set; }

    }
    public class CustomListResponse<T> : CustomResponse where T : notnull, new()
    {
        public List<T> Datas { get; set; }

    }
}
