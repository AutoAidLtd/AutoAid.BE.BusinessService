
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
                await _unitOfWork.Resolve<Place>().CreateAsync(new Place
                {
                    Lat = createData.Lat,
                    Lng = createData.Lng,
                });

                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IPagedList<PlaceDto>> SearchPlace(string keySearch, PagingQuery paginQuery, string orderbyString)
        {
            try
            {
                var result = await _unitOfWork.Resolve<Place>()
                                              .SearchAsync<PlaceDto>(keySearch, paginQuery, orderbyString);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
