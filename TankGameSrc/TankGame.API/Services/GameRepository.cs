using TankGame.API.Dbcontext;
using TankGame.API.Entities;
using System.Linq;

namespace TankGame.API.Services
{
    public class GameRepository : EFGenericRepository<GameBattle>, IGameRepository
    {
        public GameRepository(GameDbContext context) : base(context) { }

        public GameBattle GetFullGame(int gameId)
        {
            return GetAllIncluding(g => g.BlueTankModel, g => g.RedTankModel, g => g.GameMap)
               .FirstOrDefault(x => x.GameBattleId == gameId);
        }
    }
}
