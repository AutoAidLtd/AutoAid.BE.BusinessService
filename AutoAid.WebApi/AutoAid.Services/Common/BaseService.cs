using System.Net;

namespace AutoAid.Bussiness.Common
{
    public class BaseService : IDisposable
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected ApiResponse<T> Succsess<T>(T data = default(T), string? message = null)
        {
            return new ApiResponse<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Message = message
            };
        }

        protected ApiResponse<T> Failed<T>(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message
            };
        }

        #region Destructor
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseService()
        {
            Dispose(false);
        }
        #endregion Destructor
    }
}
