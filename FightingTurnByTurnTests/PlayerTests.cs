using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class PlayerTests
    {
        private Game game;
        private Player playerOne;
        private Player playerTwo;

        [TestInitialize]
        public void InitTests()
        {
            game = new Game();
            game.AddPlayer("David");
            game.AddPlayer("Divad");
            playerOne = game.Players.First();
            playerTwo = game.Players.Last();
        }

        [TestMethod]
        public void PlayerOneCanChooseAMageCharacter()
        {
            playerOne.ChooseCharacter(CharacterTypes.Mage);
            Assert.AreEqual(playerOne.NumberCharactersSelected(), 1);
        }

        [TestMethod]
        public void PlayerOneCanChooseAWarriorCharacter()
        {
            playerOne.ChooseCharacter(CharacterTypes.Warrior);
            Assert.AreEqual(playerOne.NumberCharactersSelected(), 1);
        }

        [TestMethod]
        public void PlayerOneCanChooseAHealerCharacter()
        {
            playerOne.ChooseCharacter(CharacterTypes.Healer);
            Assert.AreEqual(playerOne.NumberCharactersSelected(), 1);
        }

        [TestMethod]
        public void PlayerOneCanChooseThreeCharacters()
        {
            playerOne.ChooseCharacter(CharacterTypes.Warrior);
            playerOne.ChooseCharacter(CharacterTypes.Mage);
            playerOne.ChooseCharacter(CharacterTypes.Healer);
        }

        [TestMethod]
        public void PlayerOneChooseFourCharacters_WillThrowUUnhandledNumberOfCharactersException()
        {
            playerOne.ChooseCharacter(CharacterTypes.Warrior);
            playerOne.ChooseCharacter(CharacterTypes.Mage);
            playerOne.ChooseCharacter(CharacterTypes.Healer);
            Assert.ThrowsException<UnhandledNumberOfCharactersException>(() =>playerOne.ChooseCharacter(CharacterTypes.Healer));
        }

        [TestMethod]
        public void PlayerTwoCanChooseAMageCharacter()
        {
            playerTwo.ChooseCharacter(CharacterTypes.Mage);
            Assert.AreEqual(playerTwo.NumberCharactersSelected(), 1);
        }

        [TestMethod]
        public void PlayerTwoCanChooseAWarriorCharacter()
        {
            playerTwo.ChooseCharacter(CharacterTypes.Warrior);
            Assert.AreEqual(playerTwo.NumberCharactersSelected(), 1);
        }

        [TestMethod]
        public void PlayerTwoCanChooseAHealerCharacter()
        {
            playerTwo.ChooseCharacter(CharacterTypes.Healer);
            Assert.AreEqual(playerTwo.NumberCharactersSelected(), 1);
        }

        [TestMethod]
        public void PlayerTwoCanChooseThreeCharacters()
        {
            playerTwo.ChooseCharacter(CharacterTypes.Warrior);
            playerTwo.ChooseCharacter(CharacterTypes.Mage);
            playerTwo.ChooseCharacter(CharacterTypes.Healer);
        }

        [TestMethod]
        public void PlayerTwoChooseFourCharacters_WillThrowUUnhandledNumberOfCharactersException()
        {
            playerTwo.ChooseCharacter(CharacterTypes.Warrior);
            playerTwo.ChooseCharacter(CharacterTypes.Mage);
            playerTwo.ChooseCharacter(CharacterTypes.Healer);
            Assert.ThrowsException<UnhandledNumberOfCharactersException>(() => playerTwo.ChooseCharacter(CharacterTypes.Healer));
        }

        [TestMethod]
        public void RandomiseCharacterSelectionForPlayerOne()
        {
            playerOne.RandomiseCharacter();
            bool typeInRange = playerOne.Characters[0].GetType() == typeof(Mage) 
                || playerOne.Characters[0].GetType() == typeof(Warrior) 
                || playerOne.Characters[0].GetType() == typeof(Healer);
            Console.WriteLine(playerOne.Characters[0].GetType());
            Assert.IsTrue(typeInRange);
        }

        [TestMethod]
        public void RandomiseAllThreeCharactersSelectionForPlayerOne()
        {
            playerOne.RandomiseCharacters();
            playerOne.Characters.ForEach(character => Console.WriteLine(character.GetType()));
            Assert.AreEqual(playerOne.CountCharacters(), 3);
        }
    }
}
