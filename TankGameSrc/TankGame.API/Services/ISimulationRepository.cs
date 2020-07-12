using TankGame.API.Entities;

namespace TankGame.API.Services
{
    public interface ISimulationRepository : IEFGenericRepository<GameSimulation>
    {
        GameSimulation GetFullGameSimulation(int simulationId);
    }
}
