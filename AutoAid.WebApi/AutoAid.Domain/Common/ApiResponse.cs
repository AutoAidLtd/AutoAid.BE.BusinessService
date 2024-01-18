using System.Net;

namespace AutoAid.Domain.Common
{
    public class ApiResponse<T> 
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; } = null;
        public T? Data { get; set; } = default(T);
    }
}
