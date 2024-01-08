using AutoAid.Application.Repository;
using AutoAid.Application.Service;
using AutoAid.Bussiness.Common;
using AutoAid.Domain.Dto.Place;
using AutoAid.Infrastructure.Models;

namespace AutoAid.Bussiness.Service
{
    public class PlaceService : BaseService, IPlaceService
    {
        public PlaceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> Create(CreatePlaceDto createData)
        {
            try
            {
                var result = await _unitOfWork.Resolve<Place>().CreateAsync(new Place
                {
                    Lat = createData.Lat,
                    Lng = createData.Lng,
                }, isSaveChange: true);

                return result > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
