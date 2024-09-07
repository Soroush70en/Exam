using Domain.Context;
using System.Collections;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class UnitOFWork(ExamDbContext _context) : IUnitofWork, IDisposable
    {
        #region Variable's
        private bool disposedValue;
        private Hashtable _repositories;
        private readonly ExamDbContext Context = _context;
        #endregion

        #region Function's
        public ExamDbContext GetContext()
        {
            return Context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= [];

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type)) return (IRepository<TEntity>)_repositories[type];

            var repositoryType = typeof(Repository<>);

            var repositoryInstance =
                Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(TEntity)), Context);

            _repositories.Add(type, repositoryInstance);

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<bool> SaveChangeAsync()
        {
            bool Result = Convert.ToBoolean(await Context.SaveChangesAsync());
            return Result;
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        ~UnitOFWork()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
