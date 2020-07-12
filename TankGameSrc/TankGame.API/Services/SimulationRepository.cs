using TankGame.API.Dbcontext;
using TankGame.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TankGame.API.Services
{
    public class SimulationRepository : EFGenericRepository<GameSimulation>, ISimulationRepository
    {
        public SimulationRepository(GameDbContext context) : base(context) { }

        public GameSimulation GetFullGameSimulation(int simulationId)
        {
            return GetAllIncluding(s => s.GameBattle).FirstOrDefault(x => x.GameSimulationId == simulationId);
               
        }
    }
}
