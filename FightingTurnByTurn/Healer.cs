using System;
using System.Collections.Generic;
using System.Text;

namespace FightingTurnByTurn
{
    public class Healer : Character
    {
        public Healer()
        {
            LifePoint = 20;
            Power = 3;
            Speed = 4;
            Cooldown = 3;
        }

        public override void SpecialAttack(Character character, Character toHeal)
        {
            SimpleAttack(character);
            toHeal.LifePoint += 8;
            IsOnCooldown = true;
            NumberOfTimePlayedSinceSpecial = 0;
        }
    }
}
