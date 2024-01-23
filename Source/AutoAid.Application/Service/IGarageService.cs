using AutoAid.Domain.Common;
using AutoAid.Domain.Dto.Garage;

namespace AutoAid.Application.Service
{
    public interface IGarageService : IDisposable
    {
        Task<ApiResponse<bool>> Create(CreateGarageReq req);
    }
}
