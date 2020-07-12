using TankGame.API.Dbcontext;
using TankGame.API.Entities;

namespace TankGame.API.Services
{
    public class TankModelRepository : EFGenericRepository<TankModel>, ITankModelRepository
    {
        public TankModelRepository(GameDbContext context) : base(context) { }
    }
}
