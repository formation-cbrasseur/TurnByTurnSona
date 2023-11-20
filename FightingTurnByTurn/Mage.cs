using System;
using System.Collections.Generic;
using System.Text;

namespace FightingTurnByTurn
{
    public class Mage : Character
    {
        public Mage()
        {
            LifePoint = 24;
            Power = 6;
            Speed = 5;
            Cooldown = 2;
        }

        public override void SpecialAttack(Character character, Character toHeal)
        {
            SimpleAttack(character);
            SimpleAttack(character);
            IsOnCooldown = true;
            NumberOfTimePlayedSinceSpecial = 0;
        }
    }
}
