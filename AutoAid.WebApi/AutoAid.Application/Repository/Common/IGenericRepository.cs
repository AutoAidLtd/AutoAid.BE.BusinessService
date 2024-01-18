using AutoAid.Domain.Common.PagedList;

namespace AutoAid.Application.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Query
        Task<TEntity?> FindAsync(int entityId);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TResult>> GetAllAsync<TResult>() where TResult : class;
        Task<IPagedList<TEntity>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
        Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
            where TResult : class;
        #endregion Query

        #region Command 
        Task CreateAsync(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
        Task DeleteAsync(params int[] ids);
        #endregion Command 
    }
}
