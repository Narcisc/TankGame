using TankGame.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace TankGame.API.Dbcontext
{
    /// <summary>
    /// Database class context
    /// Contains all database objects mapped by entity framework
    /// </summary>
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options): base(options){
            
        }
        
        
        public DbSet<TankModel> TankModels { get; set; }

        
        public DbSet<GameMap> GameMaps { get; set; }

        
        public DbSet<GameBattle> GameBattles { get; set; }

        
        public DbSet<GameSimulation> GameSimulations { get; set; }

    }
}
