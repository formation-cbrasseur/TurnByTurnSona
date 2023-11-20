using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightingTurnByTurn
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public bool IsStarted { get; set; }
        public int CurrentTurn { get; set; }
        public bool HasEnded { get; set; }
        public Player Winner { get; private set; }

        public Game ()
        {
            Players = new List<Player>();
            IsStarted = false;
            CurrentTurn = 1;
            Winner = null;
        }

        public int CountPlayers()
        {
            return Players.Count;
        }

        public void AddPlayer(string name)
        {
            if (CountPlayers() == 2)
                throw new UnhandledNumberOfPlayersException("You can't play with more than two players");

            Players.Add(new Player(name));
        }

        public void Start()
        {
            AskPlayersNames();
            Players.ForEach(player => player.RandomiseCharacters());
            SetIsStarted();
        }

        public void SetIsStarted()
        {
            if (CountPlayers() != 2)
                throw new UnhandledNumberOfPlayersException("You can't start a game if there is not exactly two players");

            IsStarted = true;
        }

        public void AskPlayersNames()
        {
            Console.WriteLine("Please set player one name : ");
            AddPlayer(Console.ReadLine());
            Console.WriteLine("Please set player two name : ");
            AddPlayer(Console.ReadLine());
        }

        public Player GetPlayerTurn()
        {
            if (CurrentTurn % 2 != 0)
                return Players[0];

            return Players[1];
        }

        public Character Turn()
        {
            var player = GetPlayerTurn();

            if (player.Characters.Where(charac => charac.Alive).All(charac => charac.HasAlreadyAttack))
                player.Characters.Where(charac => charac.Alive).ToList().ForEach(charac => charac.HasAlreadyAttack = false);

            var character = player.GetFastestCharacter();
            LogTurnState(player, character);
            var otherPlayer = GetOtherPlayer(player);
            var target = GetTargetWithLessLifePoint(otherPlayer);
            var toHeal = GetTargetWithLessLifePoint(player);

            character.Attack(target, toHeal);

            if (otherPlayer.Characters.All(charac => !charac.Alive) || player.Characters.All(charac => !charac.Alive))
            {
                HasEnded = true;
                SetWinner();
                LogEndGame();
            }

            CurrentTurn++;

            return character;
        }

        public Player GetOtherPlayer(Player player)
        {
            return player == Players[0] ? Players[1] : Players[0];
        }

        public Character GetTargetWithLessLifePoint(Player player)
        {
            return player.Characters.Where(charac => charac.Alive).OrderBy(charac => charac.LifePoint).FirstOrDefault();
        }

        public void SetWinner()
        {
            if (HasEnded)
                Winner = Players.Where(charac => charac.Characters.Any(characters => characters.Alive)).First();
        }

        public void LogTurnState(Player player, Character character)
        {
            Console.WriteLine("Current turn : {0} - Player {1} is playing with {2}", 
                CurrentTurn, 
                player == Players[0] ? 1 : 2,
                character);
            Console.WriteLine("Player One : {0} | {1} | {2} ", 
                Players[0].Characters[0].LifePoint.ToString(),
                Players[0].Characters[1].LifePoint.ToString(),
                Players[0].Characters[2].LifePoint.ToString()
            );
            Console.WriteLine("Player Two : {0} | {1} | {2} ",
                Players[1].Characters[0].LifePoint.ToString(),
                Players[1].Characters[1].LifePoint.ToString(),
                Players[1].Characters[2].LifePoint.ToString()
            );
        }

        public void LogEndGame()
        {
            if (HasEnded)
            {
                Console.WriteLine("Game has ended !");
                Console.WriteLine("Player {0} won the game !", Winner.Name);
            }
        }

        public void DisplayPlayerAfterTurn()
        {
            Console.Write("Player 1 ending turn");
        }
    }
    public class UnhandledNumberOfPlayersException : Exception
    {
        public UnhandledNumberOfPlayersException(string message) : base(message)
        {
        }
    }
}
