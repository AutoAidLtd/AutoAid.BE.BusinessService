using AutoAid.Domain.Common;
using AutoAid.Domain.Dto.GarageService;

namespace AutoAid.Application.Service
{
    public interface IGarageServiceService : IDisposable
    {
        Task<ApiResponse<bool>> CreateAGarageService(CreateGarageServiceReq req);
    }
}
