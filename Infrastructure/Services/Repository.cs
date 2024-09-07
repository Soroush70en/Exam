using Domain.Context;
using Application.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
{
    #region Variable's
    private bool disposedValue;
    internal DbSet<TEntity> dbSet;
    internal ExamDbContext Context;
    #endregion

    #region Constructor's
    public Repository(ExamDbContext _context)
    {
        Context = _context;
        dbSet = Context.Set<TEntity>();
    }
    #endregion

    #region Function's
    public async Task InsertAsync(TEntity Entry)
    {
        await dbSet.AddAsync(Entry);
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> Entrys)
    {
        await dbSet.AddRangeAsync(Entrys);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null, int? Page = null, int? PerPage = null)
    {
        IQueryable<TEntity> Query = dbSet;
        Page ??= 1;
        PerPage ??= 25;

        if (Filter != null)
        {
            Query = Query.Where(Filter).Skip((Page.Value - 1) * PerPage.Value).Take(PerPage.Value);
        }

        if (OrderBy != null)
        {
            return await OrderBy(Query).ToListAsync();
        }
        else
        {
            return await Query.ToListAsync();
        }
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> Filter = null)
    {
        IQueryable<TEntity> Query = dbSet;

        if (Filter != null)
        {
            return await Query.CountAsync(Filter);
        }

        return await Query.CountAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> Filter = null)
    {
        return await dbSet.SingleOrDefaultAsync(Filter);
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> Filter = null)
    {
        return await dbSet.FirstOrDefaultAsync(Filter);
    }

    public async Task<TEntity> FindAsync(object Id)
    {
        return await dbSet.FindAsync(Id);
    }

    public Task UpdateAsync(TEntity Entry)
    {
        dbSet.Update(Entry);
        return Task.CompletedTask;
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> Entrys)
    {
        dbSet.UpdateRange(Entrys);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(object Id)
    {
        TEntity Entry = await FindAsync(Id);
        await DeleteAsync([Entry]);
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> Entrys)
    {
        await DeleteAsync(Entrys);
    }

    private Task DeleteAsync(IEnumerable<TEntity> Entry)
    {
        foreach (var item in Entry)
        {
            dbSet.Entry(item).State = EntityState.Deleted;
        }
        return Task.CompletedTask;
    }
    #endregion

    #region Idisposable
    protected void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Context.Dispose();
                dbSet = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    ~Repository()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
