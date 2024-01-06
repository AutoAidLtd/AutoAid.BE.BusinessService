using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Application.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        TEntityRepository? GetRepository<TEntityRepository, TEntity>()
            where TEntity : class
            where TEntityRepository : IGenericRepository<TEntity>;
    }
}
