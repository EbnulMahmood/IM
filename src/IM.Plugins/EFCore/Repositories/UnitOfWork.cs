using IM.Plugins.EFCore.Data;
using IM.UseCases.PluginInterfaces;
using Microsoft.Extensions.Logging;

namespace IM.Plugins.EFCore.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool _disposed = false;
        private readonly InventoryDbContext _context;
        private readonly ILogger _logger;

        public ICategoryRepository CategoryRepository { get; private set; }

        public UnitOfWork(InventoryDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            CategoryRepository = new CategoryRepository(context, _logger);
        }

        public async Task<bool> SaveAsync()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }
            return returnValue;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}