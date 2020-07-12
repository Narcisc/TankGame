using TankGame.API.Dbcontext;
using System;
using System.Threading.Tasks;

namespace TankGame.API.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private GameDbContext _context;

        public UnitOfWork(GameDbContext context)
        {
            _context = context;
        }

      
        public bool SaveChanges()
        {
            var noObjects = _context.SaveChanges();
            return noObjects > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
