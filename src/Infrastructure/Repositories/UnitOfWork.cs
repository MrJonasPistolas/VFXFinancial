﻿namespace VFXFinancial.Infrastructure.Repositories;
public class UnitOfWork<TId> : IUnitOfWork<TId>
{
    private readonly ApplicationDbContext _dbContext;
    private bool disposed;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IRepositoryAsync<TEntity, TId> Repository<TEntity>() where TEntity : AuditableEntity<TId>
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryAsync<,>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TId)), _dbContext);

            _repositories.Add(type, repositoryInstance);
        }

        return (IRepositoryAsync<TEntity, TId>)_repositories[type];
    }

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task Rollback()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _dbContext.Dispose();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }
}