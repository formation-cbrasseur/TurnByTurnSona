using System;
using System.Collections.Generic;
using System.Linq;

namespace FightingTurnByTurn
{
    public class Player
    {
        public string Name { get; set; }
        public List<Character> Characters { get; set; }
        public int NumberOfCharacters { get; set; }

        public Player(string name)
        {
            Name = name;
            Characters = new List<Character>();
            NumberOfCharacters = 3;
        }

        public void ChooseCharacter(CharacterTypes type)
        {
            if (NumberCharactersSelected() == 3)
                throw new UnhandledNumberOfCharactersException("You can't choose more than three characters");

            if (type == CharacterTypes.Mage)
                Characters.Add(new Mage());
            else if (type == CharacterTypes.Warrior)
                Characters.Add(new Warrior());
            else if (type == CharacterTypes.Healer)
                Characters.Add(new Healer());
        }

        public int NumberCharactersSelected()
        {
            return Characters.Count;
        }

        public void RandomiseCharacter()
        {
            Array values = Enum.GetValues(typeof(CharacterTypes));
            Random random = new Random();
            CharacterTypes randomCharacterType = (CharacterTypes)values.GetValue(random.Next(values.Length));
            ChooseCharacter(randomCharacterType);
        }

        public void RandomiseCharacters()
        {
            for (var i = 0; i < NumberOfCharacters; i++)
                RandomiseCharacter();
        }

        public int CountCharacters()
        {
            return Characters.Count;
        }

        public Character GetFastestCharacter()
        {
            return Characters.OrderByDescending(character => character.Speed).Where(charac => !charac.HasAlreadyAttack && charac.Alive).FirstOrDefault();
        }
    }

    public class UnhandledNumberOfCharactersException : Exception
    {
        public UnhandledNumberOfCharactersException(string message) : base(message)
        {
        }
    }
}