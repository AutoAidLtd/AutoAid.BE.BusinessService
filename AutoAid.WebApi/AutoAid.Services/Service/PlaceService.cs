using AutoAid.Application.Repository;
using AutoAid.Application.Service;
using AutoAid.Bussiness.Common;
using AutoAid.Infrastructure.Models;

namespace AutoAid.Bussiness.Service
{
    public class PlaceService : BaseService, IPlaceService
    {
        public PlaceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> Create(double lat, double lng)
        {
            try
            {
                var result = await _unitOfWork.GetRepository<Place>().CreateAsync(new Place
                {
                    Lat = lat,
                    Lng = lng
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
