using AutoAid.Domain.Common.PagedList;
using AutoAid.Domain.Model;
using AutoAid.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Infrastructure.Repository
{
    internal class GarageRepostiory : GenericRepository<Garage>
    {
        public GarageRepostiory(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Garage>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
