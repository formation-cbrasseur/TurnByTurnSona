using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class MageTests
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
            playerOne.ChooseCharacter(CharacterTypes.Mage);
            character = playerOne.Characters.First();
        }

        [TestMethod]
        public void MageCharacterHaveTheRightStats()
        {
            Assert.AreEqual(character.Speed, 5);
            Assert.AreEqual(character.Power, 6);
            Assert.AreEqual(character.LifePoint, 24);
        }
    }
}
