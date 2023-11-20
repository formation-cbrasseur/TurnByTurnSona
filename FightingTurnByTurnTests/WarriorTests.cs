using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class WarriorTests
    {
        private Game game;
        private Player playerOne;
        private Character character;

        [TestInitialize]
        public void InitTests()
        {
            game = new Game();
            game.AddPlayer("David");
            game.AddPlayer("Divad");
            playerOne = game.Players.First();
            playerOne.ChooseCharacter(CharacterTypes.Warrior);
            character = playerOne.Characters.First();
        }

        [TestMethod]
        public void WarriorCharacterHaveTheRightStats()
        {
            Assert.AreEqual(character.Speed, 6);
            Assert.AreEqual(character.Power, 5);
            Assert.AreEqual(character.LifePoint, 30);
        }
    }
}
