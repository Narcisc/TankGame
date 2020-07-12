using TankGame.API.Dbcontext;
using TankGame.API.Entities;
using TankGame.API.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TankGame.API.Services
{
    public class GameMapRepository : EFGenericRepository<GameMap>, IGameMapRepository
    {

        public GameMapRepository(GameDbContext context) : base(context) { }

        public async Task<IEnumerable<GameMap>> GetByHeight(int height)
        {
            return await GetWhere(x => x.Height == height );
        }

        public async Task<IEnumerable<GameMap>> GetByWidth(int width)
        {
            return await GetWhere(x => x.Width == width);
        }

        public async Task<GameMap> SetObstacleAsync(int gameMapId, int posX, int posY)
        {
            return await SetValue(gameMapId, posX, posY, 1);
        }
        public async Task<GameMap> RemoveObstacleAsync(int gameMapId, int posX, int posY)
        {
            return await SetValue(gameMapId, posX, posY, 0);
        }

        private async Task<GameMap> SetValue(int gameMapId, int posX, int posY, int value)
        {
            var exist = await GetById(gameMapId);
            
            if (posX < 0 || posY < 0 || posX >= exist.Height || posY >= exist.Width) return null;
            if (exist == null) return null;
            var matrix = exist.Map.ToMatrix();

            matrix[posX][posY] = value;
            if (value == 1) 
                exist.NoObstacles++;
            else 
                exist.NoObstacles--;
            exist.Map = matrix.ToJson();

            return await UpdateAsync(exist, exist.GameMapId);
        }

        
    }
}
