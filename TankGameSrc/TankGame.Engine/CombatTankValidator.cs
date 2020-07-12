using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame.Engine
{
    public class CombatTankValidator : AbstractValidator<CombatTank> 
    {
        public CombatTankValidator()
        {
            RuleFor(tank => tank.ShieldLife).GreaterThan(0);
            RuleFor(tank => tank.GunPower).GreaterThan(0);
            RuleFor(tank => tank.GunRange).GreaterThan(0);
            RuleFor(tank => tank.Speed).GreaterThan(0);
            RuleFor(tank => tank.PosX).GreaterThan(-1);
            RuleFor(tank => tank.PosY).GreaterThan(-1);
        }
    }
}
