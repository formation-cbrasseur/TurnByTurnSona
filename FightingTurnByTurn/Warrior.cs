using System;
using System.Collections.Generic;
using System.Text;

namespace FightingTurnByTurn
{
    public class Warrior : Character
    {

        public Warrior()
        {
            LifePoint = 30;
            Power = 5;
            Speed = 6;
            Cooldown = 2;
        }

        public override void SpecialAttack(Character character, Character toHeal)
        {
            SimpleAttack(character);
            HasShield = true;
            IsOnCooldown = true;
            NumberOfTimePlayedSinceSpecial = 0;
        }
    }
}
