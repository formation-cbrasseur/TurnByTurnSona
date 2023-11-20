using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class GameTests
    {
        private Game game;

        [TestInitialize]
        public void InitTests()
        {
            game = new Game();
        }

        [TestMethod]
        public void StartingGameNoPlayers()
        {
            Assert.AreEqual(game.CountPlayers(), 0);
        }

        [TestMethod]
        public void AddingOnePlayers_WillGiveOnePlayersCount()
        {
            game.AddPlayer("David");
            Assert.AreEqual(game.CountPlayers(), 1); 
        }

        [TestMethod]
        public void AddingTwoPlayers_WillGiveTwoPlayersCount()
        {
            game.AddPlayer("David");
            game.AddPlayer("Divad");
            Assert.AreEqual(game.CountPlayers(), 2);
        }

        [TestMethod]
        public void AddingThreePlayers_WillThrowUnhandleNumberOfPlayersException()
        {
            game.AddPlayer("David");
            game.AddPlayer("Divad");
            Assert.ThrowsException<UnhandledNumberOfPlayersException>(() => game.AddPlayer("Jean"));
        }

        [TestMethod]
        public void AskPlayersNames_WillSetPlayerNames()
        {
            var input = new StringReader(@"David
Divad");
            Console.SetIn(input);

            game.AskPlayersNames();

            Assert.AreEqual(game.Players[0].Name, "David");
            Assert.AreEqual(game.Players[1].Name, "Divad");
        }

        [TestMethod]
        public void StartGame_WillHaveTwoPlayers()
        {
            game.Start();
            Assert.AreEqual(game.CountPlayers(), 2);
        }

        [TestMethod]
        public void SetIsStarted_SetItToTrue()
        {
            game.AddPlayer("David");
            game.AddPlayer("Divad");
            game.SetIsStarted();
            Assert.IsTrue(game.IsStarted);
        }

        [TestMethod]
        public void StartGameWithTwoPlayers_WillSetGameStarted()
        {
            game.Start();
            Assert.IsTrue(game.IsStarted);
        }

        [TestMethod]
        public void StartGame_SetTurnToOne()
        {
            game.Start();
            Assert.AreEqual(game.CurrentTurn, 1);
        }

        [TestMethod]
        public void GetPlayerTurn()
        {
            game.Start();
            Assert.AreEqual(game.GetPlayerTurn(), game.Players[0]);
        }

        [TestMethod]
        public void Z_AfterFirstTurn_CurrentTurnEqualTwo()
        {
            game.Start();
            game.Turn();
            Assert.AreEqual(game.CurrentTurn, 2);
        }

        [TestMethod]
        public void ForSecondTurn_GetPlayerTurnReturnPlayerTwo()
        {
            game.Start();
            game.Turn();
            Assert.AreEqual(game.GetPlayerTurn(), game.Players[1]);
        }

        [TestMethod]
        public void ForThirdTurn_GetPlayerTurnReturnPlayerOne()
        {
            game.Start();
            game.Turn();
            game.Turn();
            Assert.AreEqual(game.GetPlayerTurn(), game.Players[0]);
        }

        [TestMethod]
        public void GameEndWhenAllCharactersOfAPlayerAreNotAlive()
        {
            var input = new StringReader(@"David
Divad");
            Console.SetIn(input);

            game.Start();
            while(!game.HasEnded)
            {
                game.Turn();
            }
        }

        [TestMethod]
        public void GetOtherPlayerTests()
        {
            game.Start();
            var playerOne = game.Players[0];
            var playerTwo = game.Players[1];

            Assert.AreEqual(game.GetOtherPlayer(playerOne), playerTwo);
            Assert.AreEqual(game.GetOtherPlayer(playerTwo), playerOne);
        }

        [TestMethod]
        public void GetTargetWithLessLifePoint_WillGiveTargetWithLessLifePoint()
        {
            game.Start();
            var playerOne = game.Players[0];
            var targetMinLifePointCharacter = playerOne.Characters.Where(charac => charac.Alive).OrderBy(charac => charac.LifePoint).First();

            Assert.AreEqual(game.GetTargetWithLessLifePoint(playerOne), targetMinLifePointCharacter);
        }

        [TestMethod]
        public void KillPlayerOne_WillGivePlayerTwoHasWinner()
        {
            game.Start();
            game.Players[0].Characters.ForEach(charac => charac.LifePoint = 0);
            game.HasEnded = true;
            game.SetWinner();

            Assert.AreEqual(game.Winner, game.Players[1]);
        }

        [TestMethod]
        public void KillPlayerTwo_WillGivePlayerOneHasWinner()
        {
            game.Start();
            game.Players[1].Characters.ForEach(charac => charac.LifePoint = 0);
            game.HasEnded = true;
            game.SetWinner();

            Assert.AreEqual(game.Winner, game.Players[0]);
        }

        [TestMethod]
        public void CurrentPlayerCanBeDisplayedAtEndOfTurn()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            game.DisplayPlayerAfterTurn();
            Assert.AreEqual("Player 1 ending turn", sw.ToString());

        }
    }
}
