using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankGame.API.Entities
{
    /// <summary>
    /// Base entity for all entities
    /// </summary>
    public abstract class BaseEntity
    {
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
