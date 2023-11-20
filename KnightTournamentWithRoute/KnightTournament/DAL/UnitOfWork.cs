namespace KnightTournament.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _appContext = new ApplicationDbContext();
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return new Repository<TEntity>(_appContext);
        }

        public UnitOfWork(ApplicationDbContext appManagerContext) { _appContext = appManagerContext; }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
