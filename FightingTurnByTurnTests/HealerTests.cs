using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class HealerTests
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
            playerOne.ChooseCharacter(CharacterTypes.Healer);
            character = playerOne.Characters.First();
        }

        [TestMethod]
        public void HealerCharacterHaveTheRightStats()
        {
            Assert.AreEqual(character.Speed, 4);
            Assert.AreEqual(character.Power, 3);
            Assert.AreEqual(character.LifePoint, 20);
        }
    }
}
