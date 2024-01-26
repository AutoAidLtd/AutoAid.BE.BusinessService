using AutoAid.Application.Common;
using AutoAid.Domain.Dto.GarageService;
using Mapster;

namespace AutoAid.Bussiness.Service
{
    public class GarageServiceService : BaseService, IGarageServiceService
    {
        public GarageServiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> CreateAGarageService(CreateGarageServiceReq req)
        {
            try
            {
                var garageService = req.Adapt<GarageService>();

                ArgumentNullException.ThrowIfNull(garageService);

                await _unitOfWork.Resolve<GarageService>().CreateAsync(garageService);
                var result = await _unitOfWork.SaveChangesAsync();

                return Success(result > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }
    }
}
