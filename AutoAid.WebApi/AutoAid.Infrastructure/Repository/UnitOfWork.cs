using AutoAid.Application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoAid.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private readonly Dictionary<Type, object> _repositoryDictionary;
    private IDbContextTransaction? transaction;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        _repositoryDictionary = new Dictionary<Type, object>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        if (transaction != null)
            throw new InvalidOperationException("Transaction has already been started.");

        transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            if (transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (transaction == null)
            throw new InvalidOperationException("Transaction has not been started.");

        await transaction.RollbackAsync();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var respository = _repositoryDictionary.GetValueOrDefault(typeof(TEntity));
        if (respository == null)
        {
            respository = new GenericRepository<TEntity>(_dbContext);
            _repositoryDictionary.Add(typeof(TEntity), respository);
        }
        return (IGenericRepository<TEntity>)respository;
    }

    public TEntityRepository? GetRepository<TEntityRepository, TEntity>()
        where TEntity : class
        where TEntityRepository : IGenericRepository<TEntity>
    {
        var respository = _repositoryDictionary.GetValueOrDefault(typeof(TEntity));

        if (respository == null)
        {
            respository = Activator.CreateInstance(typeof(TEntityRepository), _dbContext);

            if (respository == null)
                return default;

            _repositoryDictionary.Add(typeof(TEntity), respository);
        }

        return (TEntityRepository)respository;
    }

    #region Destructor
    private bool isDisposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
            return;

        if (disposing)
        {
            _dbContext.Dispose();
        }

        isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
    #endregion Destructor
}

