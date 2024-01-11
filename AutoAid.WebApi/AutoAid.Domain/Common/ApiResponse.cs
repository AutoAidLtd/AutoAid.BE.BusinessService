using System.Net;

namespace AutoAid.Domain.Common
{
    public class ApiResponse<TData> where TData : class, new()
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; } = null;
        public TData? Data { get; set; } = null;
    }
}
