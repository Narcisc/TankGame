using TankGame.API.Entities;


namespace TankGame.API.Services
{
    public interface  IGameRepository : IEFGenericRepository<GameBattle>
    {
        GameBattle GetFullGame(int gameId);
    }
}
