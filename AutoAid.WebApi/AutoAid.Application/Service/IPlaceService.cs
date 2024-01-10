using AutoAid.Domain.Common;
using AutoAid.Domain.Dto.Place;
using AutoAid.Infrastructure.Repository.Helper;

namespace AutoAid.Application.Service
{
    public interface IPlaceService : IDisposable
    {
        Task<bool> Create(CreatePlaceDto createData);
        Task<IPagedList<PlaceDto>> SearchPlace(string keySearch, PagingQuery paginQuery, string orderbyString);
    }
}
