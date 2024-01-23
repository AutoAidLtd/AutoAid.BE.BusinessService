using AutoAid.Domain.Common.PagedList;
using AutoAid.Domain.Model;
using AutoAid.Infrastructure.Repository.Common;
using AutoAid.Infrastructure.Repository.Helper;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Infrastructure.Repository
{
    public class PlaceRepository : GenericRepository<Place>
    {
        public PlaceRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IPagedList<Place>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            var lat = double.Parse(keySearch);
            var lng = double.Parse(keySearch);

            return await _dbSet.AsNoTracking()
                        .WhereWithExist(p => p.Lat == lat || p.Lng == lng)
                        .AddOrderByString(orderBy)
                        .ToPagedListAsync(pagingQuery);
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string? keySearch, PagingQuery pagingQuery, string? orderBy)
        {
            double lat = 0, lng = 0;

            if (!string.IsNullOrEmpty(keySearch))
            {
                lat = double.Parse(keySearch);
                lng = double.Parse(keySearch);
            }

            return await _dbSet.AsNoTracking()
                        .WhereWithExist(p => string.IsNullOrEmpty(keySearch) || (p.Lat == lat || p.Lng == lng))
                        .AddOrderByString(orderBy)
                        .SelectWithField<Place, TResult>()
                        .ToPagedListAsync(pagingQuery);

        }
    }
}
