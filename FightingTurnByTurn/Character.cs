using System;

namespace FightingTurnByTurn
{
    public abstract class Character
    {
        public int LifePoint { get; set; }
        public int Power { get; set; }
        public int Speed { get; set; }
        public bool HasAlreadyAttack { get; set; }
        public bool Alive { get { return LifePoint > 0; } }
        public bool IsOnCooldown { get; set; }
        public int Cooldown { get; set; }
        public int NumberOfTimePlayedSinceSpecial { get; set; }
        public bool HasShield { get; set; }

        protected Character()
        {
            HasAlreadyAttack = false;
            IsOnCooldown = false;
            NumberOfTimePlayedSinceSpecial = 0;
            HasShield = false;
        }

        public abstract void SpecialAttack(Character character, Character toHeal);

        public void SimpleAttack(Character character)
        {
            character.LifePoint -= character.HasShield ? (int) Decimal.Round(Power / 2) : Power;
            character.HasShield = false;
            HasAlreadyAttack = true;
            NumberOfTimePlayedSinceSpecial += 1;
        }

        public void Attack(Character character, Character toHeal)
        {
            if (!character.Alive)
                throw new CannotAttackDeadCharacterException("Target someone alive please...");

            if (!IsOnCooldown)
                SpecialAttack(character, toHeal);
            else
                SimpleAttack(character);

            ResetCooldown();
        }

        public void ResetCooldown()
        {
            if (NumberOfTimePlayedSinceSpecial == Cooldown)
                IsOnCooldown = false;
        }
    }

    public class CannotAttackDeadCharacterException : Exception
    {
        public CannotAttackDeadCharacterException(string message) : base(message)
        {
        }
    }
}