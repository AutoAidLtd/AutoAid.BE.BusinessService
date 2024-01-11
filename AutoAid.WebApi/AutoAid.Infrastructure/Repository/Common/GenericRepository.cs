using AutoAid.Application.Repository;
using AutoAid.Domain.Common;
using AutoAid.Domain.Common.PagedList;
using AutoAid.Infrastructure.Repository.Helper;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Infrastructure.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> FindAsync(int entityId)
        {
            return await dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.WhereWithExist(string.Empty)
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : class
        {
            return await dbSet.WhereWithExist(string.Empty)
                                .SelectWithField<TEntity, TResult>()
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public abstract Task<IPagedList<TEntity>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);

        public abstract Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
            where TResult : class;

        public async Task CreateAsync(params TEntity[] entities)
        {
            await dbSet.AddRangeAsync(entities)
                       .ConfigureAwait(false);
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            var accessPropertyDelegate = EFRepositoryHelpers.GenerateAccessPropertyDelegate<TEntity, bool>(typeof(TEntity), "IsDeleted");
            var idsString = string.Join(",", ids);
            var condition = $"e.{EFRepositoryHelpers.GetPrimaryKeyName<TEntity>()}=ANY([{idsString}])";

            var rowEffect = await dbSet.WhereWithExist(condition)
                                       .ExecuteUpdateAsync(setPropCalls => setPropCalls.SetProperty(accessPropertyDelegate, true));
        }
    }
}
