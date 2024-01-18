using AutoAid.Domain.Common;
using AutoAid.Domain.Common.PagedList;
using AutoAid.Domain.Dto.Place;

namespace AutoAid.Application.Service
{
    public interface IPlaceService : IDisposable
    {
        Task<ApiResponse<bool>> Create(CreatePlaceDto createData);
        Task<ApiResponse<IPagedList<PlaceDto>>> SearchPlace(string keySearch, PagingQuery paginQuery, string orderbyString);
    }
}
