using AutoAid.Application.Repository;
using AutoAid.Domain.Common.PagedList;
using AutoAid.Infrastructure.Repository.Helper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AutoAid.Infrastructure.Repository.Common
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DapperClient? _dapperDAO = null;

        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        private IDbConnection DbConnection => _dbContext.Database.GetDbConnection();

        protected DapperClient DapperDAO => _dapperDAO ??= new DapperClient(DbConnection);

        public async Task<TEntity?> FindAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.WhereWithExist(string.Empty)
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : class
        {
            return await _dbSet.WhereWithExist(string.Empty)
                                .SelectWithField<TEntity, TResult>()
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public abstract Task<IPagedList<TEntity>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);

        public abstract Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
            where TResult : class;

        public async Task CreateAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            var accessPropertyDelegate = EFRepositoryHelpers.GenerateAccessPropertyDelegate<TEntity, bool>(typeof(TEntity), "IsDeleted");
            var idsString = string.Join(",", ids);
            var condition = $"e.{EFRepositoryHelpers.GetPrimaryKeyName<TEntity>()}=ANY([{idsString}])";

            var rowEffect = await _dbSet.WhereWithExist(condition)
                                        .ExecuteUpdateAsync(setPropCalls => setPropCalls.SetProperty(accessPropertyDelegate, true));
        }
    }
}
