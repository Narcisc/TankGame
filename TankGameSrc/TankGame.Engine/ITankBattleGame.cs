using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame.Engine
{
    public interface ITankBattleGame
    {
        List<GameState> SimulateBattle();
    }
}
