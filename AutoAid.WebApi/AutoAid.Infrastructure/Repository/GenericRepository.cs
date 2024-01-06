using AutoAid.Application.Repository;
using AutoAid.Domain.Common;
using AutoAid.Domain.Common.PagedList;
using AutoAid.Domain.RepositoryHelper;
using AutoAid.Infrastructure.Repository.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
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

        public async Task<IEnumerable<TEntity>?> FindRangeAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.Where(filter).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(QueryHelper<TEntity> queryHelper)
        {
            return await dbSet.ToQueryable(queryHelper)
                              .FirstOrDefaultAsync()
                              .ConfigureAwait(false);
        }

        public async Task<TSource?> GetAsync<TSource>(QueryHelper<TEntity, TSource> queryHelper) where TSource : class
        {
            return await dbSet.ToQueryable(queryHelper)
                              .FirstOrDefaultAsync()
                              .ConfigureAwait(false);
        }

        public async Task<IPagedList<TEntity>?> SearchAsync(SearchQueryHelper<TEntity> queryHelper)
        {
            var queryable = dbSet.ToQueryable(queryHelper);
            var pagedList = new PagedList<TEntity>();
            await pagedList.LoadData(queryable, queryHelper.PagingQuery);
            return pagedList;
        }

        public async Task<IPagedList<TResult>?> SearchAsync<TResult>(SearchQueryHelper<TEntity, TResult> queryHelper) where TResult : class
        {
            var queryable = dbSet.ToQueryable(queryHelper);
            var pagedList = new PagedList<TResult>();
            await pagedList.LoadData(queryable, queryHelper.PagingQuery);
            return pagedList;
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.AsNoTracking()
                          .AnyAsync(filter)
                          .ConfigureAwait(false);
        }

        public async Task<int> CreateAsync(TEntity entity, bool isSaveChange = false)
        {
            await dbSet.AddAsync(entity)
                       .ConfigureAwait(false);

            if (isSaveChange)
            {
                return await _dbContext.SaveChangesAsync()
                                         .ConfigureAwait(false);
            }

            return 0;
        }

        public async Task<int> CreateRangeAsync(IEnumerable<TEntity> entities, bool isSaveChange = false)
        {
            await dbSet.AddRangeAsync(entities)
                         .ConfigureAwait(false);

            if (isSaveChange)
            {
                return await _dbContext.SaveChangesAsync()
                                         .ConfigureAwait(false);
            }

            return 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity, bool isSaveChange = false)
        {
            dbSet.Update(entity);

            if (isSaveChange)
            {
                return await _dbContext.SaveChangesAsync()
                                       .ConfigureAwait(false) > 0;
            }

            return false;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, bool isSaveChange = false)
        {
            dbSet.UpdateRange(entities);

            if (isSaveChange)
            {
                return await _dbContext.SaveChangesAsync()
                                       .ConfigureAwait(false) > 0;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Func<TEntity, bool> filter, bool isSaveChange = false)
        {
            dbSet.RemoveRange(dbSet.Where(filter));

            if (isSaveChange)
            {
                return await _dbContext.SaveChangesAsync()
                                       .ConfigureAwait(false) > 0;
            }

            return false;
        }
    }
}
