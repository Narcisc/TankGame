using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame.Engine
{
    public class TankBattleGameValidator : AbstractValidator<TankBattleGame> 
    {
        private const int MaxWidth = 36;
        private const int MaxHeight = 36;

        public TankBattleGameValidator()
        {
            RuleFor(tankGame => tankGame.BlueTank.PosY).LessThan(tankGame => tankGame.GameMap[0].Length);
            RuleFor(tankGame => tankGame.RedTank.PosY).LessThan(tankGame => tankGame.GameMap[0].Length);
            RuleFor(tankGame => tankGame.GameMap.Length).GreaterThan(0).LessThan(MaxHeight);
            RuleFor(tankGame => tankGame.GameMap[0].Length).GreaterThan(0).LessThan(MaxWidth);
        }
    }
}
