using AutoAid.Domain.Common;

namespace AutoAid.Application.Service
{
    public interface IAuthenticationService : IDisposable
    {
        Task<ApiResponse<string>> GenerateAccessToken(string uid);
        Task<ApiResponse<bool>> ValidateAccessToken(string token);
        Task<ApiResponse<bool>> SyncAccountWithFirebase();
    }
}
