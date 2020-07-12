using System.Threading.Tasks;

namespace TankGame.API.Services
{
    public interface IUnitOfWork
    {
        bool SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
