using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame.Engine
{
    public class TankModelValidator : AbstractValidator<TankModel>
    {
        public TankModelValidator()
        {
            RuleFor(tankModel => tankModel.ShieldLife).GreaterThan(0);
            RuleFor(tankModel => tankModel.GunPower).GreaterThan(0);
            RuleFor(tankModel => tankModel.GunRange).GreaterThan(0);
            RuleFor(tankModel => tankModel.Speed).GreaterThan(0);
        }
    }
}
