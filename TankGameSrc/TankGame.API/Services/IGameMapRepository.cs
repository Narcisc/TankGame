using TankGame.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TankGame.API.Services
{
    public interface IGameMapRepository : IEFGenericRepository<GameMap>
    {
        Task<IEnumerable<GameMap>> GetByWidth(int width);
        Task<IEnumerable<GameMap>> GetByHeight(int height);
        Task<GameMap> SetObstacleAsync(int gameMapId, int posX, int posY);
        Task<GameMap> RemoveObstacleAsync(int gameMapId, int posX, int posY);
    }
}
