using AutoAid.Domain.Common;
using AutoAid.Domain.RepositoryHelper;
using AutoAid.Infrastructure.Repository.Helper;
using System.Linq.Expressions;

namespace AutoAid.Application.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Query
        public Task<TEntity?> FindAsync(int entityId);
        public Task<IEnumerable<TEntity>?> FindRangeAsync(Expression<Func<TEntity, bool>> filter);

        public Task<TEntity?> GetAsync(QueryHelper<TEntity> queryHelper);
        public Task<TSource?> GetAsync<TSource>(QueryHelper<TEntity, TSource> queryHelper) where TSource : class;

        public Task<IPagedList<TEntity>?> SearchAsync(SearchQueryHelper<TEntity> queryHelper);
        public Task<IPagedList<TResult>?> SearchAsync<TResult>(SearchQueryHelper<TEntity, TResult> queryHelper) where TResult : class;
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
        #endregion Query

        #region Command 
        public Task<int> AddAsync(TEntity entity, bool isSaveChange = false);
        public Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool isSaveChange = false);

        public Task<bool> UpdateAsync(TEntity entity, bool isSaveChange = false);
        public Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, bool isSaveChange = false);

        public Task<bool> DeleteAsync(Func<TEntity, bool> filter, bool isSaveChange = false);
        #endregion Command 
    }
}
