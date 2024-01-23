
using AutoAid.Application.Common;

namespace AutoAid.Bussiness.Service
{
    public class PlaceService : BaseService, IPlaceService
    {
        public PlaceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> Create(CreatePlaceDto createData)
        {
            try
            {
                await _unitOfWork.Resolve<Place>().CreateAsync(new Place
                {
                    Lat = createData.Lat,
                    Lng = createData.Lng,
                });

                var exResult = await _unitOfWork.SaveChangesAsync() > 0;

                return Success(exResult);
            }
            catch (Exception ex)
            {
                return Failed<bool>(message: ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<IPagedList<PlaceDto>>> SearchPlace(string? keySearch, PagingQuery paginQuery, string? orderbyString)
        {
            try
            {
                var result = await _unitOfWork.Resolve<Place>().SearchAsync<PlaceDto>(keySearch, paginQuery, orderbyString);
                return Success(result);
            }
            catch (Exception ex)
            {
                return Failed<IPagedList<PlaceDto>>(ex.GetExceptionMessage());
            }
        }
    }
}
