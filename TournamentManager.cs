using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilliardsGame
{
    public class TournamentManager
    {
        private List<Player> tournamentQueue = new List<Player>();
        private List<Tournament> tournaments = new List<Tournament>();
        private Random random = new Random();

        public void JoinTournament(Player player)
        {
            tournamentQueue.Add(player);
            Console.WriteLine($"{player.Name} has joined the tournament queue.");
        }

        public async Task MatchPlayersAsync()
        {
            Console.WriteLine("Matching players for tournaments...");
            await Task.Delay(2000); // Simulate delay for matching players

            if (tournamentQueue.Count >= 2)
            {
                while (tournamentQueue.Count >= 2)
                {
                    Player player1 = tournamentQueue[0];
                    Player player2 = tournamentQueue[1];
                    tournamentQueue.RemoveRange(0, 2);

                    Tournament tournament = new Tournament(player1, player2);
                    tournaments.Add(tournament);
                    Console.WriteLine($"Matched {player1.Name} with {player2.Name} for a tournament.");
                }
            }
            else
            {
                Console.WriteLine("Not enough players to start a tournament.");
            }
        }

        public void CompleteTournament(Tournament tournament)
        {
            Player winner = random.Next(2) == 0 ? tournament.Player1 : tournament.Player2;
            Console.WriteLine($"Tournament completed. Winner: {winner.Name}");
            winner.Rewards += tournament.Reward;
            Console.WriteLine($"{winner.Name} received reward of {tournament.Reward} points.");
            tournaments.Remove(tournament);
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int Rewards { get; set; }
    }

    public class Tournament
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public int Reward { get; private set; } = 100;

        public Tournament(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            TournamentManager tournamentManager = new TournamentManager();

            Player player1 = new Player { Name = "John" };
            Player player2 = new Player { Name = "Jane" };
            Player player3 = new Player { Name = "Alice" };

            tournamentManager.JoinTournament(player1);
            tournamentManager.JoinTournament(player2);
            tournamentManager.JoinTournament(player3);

            await tournamentManager.MatchPlayersAsync();

            foreach (var tournament in tournamentManager.tournaments)
            {
                tournamentManager.CompleteTournament(tournament);
            }
        }
    }
}
