using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.Helpers
{
    public static class GamesHelper
    {
        private const int MAX_LOBBIES_COUNT = 100;
        private static List<Game> _GameList { get; set; }

        private static object _GamesLock = new object();
        private static List<Game> _GetGames()
        {
            if (_GameList == null)
            {
                lock (_GamesLock)
                {
                    if (_GameList == null)
                    {
                        _GameList = new List<Game>();
                    }
                }
            }
            return _GameList;
        }

        private static void _AddGameToList(Game newGame)
        {
            lock (_GamesLock)
            {
                _GetGames().Add(newGame);
            }
        }

        private static bool _LobbiesFull()
        {
            lock (_GamesLock)
            {
                return _GetGames().Count >= MAX_LOBBIES_COUNT;
            }
        }

        public static Game CreateNewGame(Player player)
        {
            if (_LobbiesFull())
                throw new Exception("Lobbies are full"); //TODO handle error

            var newGame = new Game(player);
            _AddGameToList(newGame);

            return newGame;
        }

        public static Game GetGameByID(string id)
        {
            return _GetGames().FirstOrDefault(g => g.Id == id);
        }

        public static void AddPlayerToGame(Game game, Player player)
        {
            lock (_GamesLock)
            {
                game.Players.Add(player);
            }
        }

        public static void RemovePlayerFromGame(string playerID, string lobbyID)
        {
            var games = _GetGames();
            lock (_GamesLock)
            {
                var game = games.FirstOrDefault(l => l.Id == lobbyID);
                game.Players.RemoveAll(p => p.PlayerID == playerID);
                if (game.Players.Count == 0) games.Remove(game);
            }
        }

        public static void MakeNewLider(string playerID, string lobbyID)
        {
            var games = _GetGames();
            lock (_GamesLock)
            {
                games.FirstOrDefault(g => g.Id == lobbyID).Players.FirstOrDefault(p => p.PlayerID == playerID).GameLider = true;
            }
        }

        public static void StartGame(string lobbyID)
        {
            var games = _GetGames();
            lock (_GamesLock)
            {
                games.FirstOrDefault(g => g.Id == lobbyID).State = GameState.Started;
            }
        }

        public static string GenerateGameID()
        {
            var random = new Random();
            int newID;
            bool newIdFound = false;

            var games = _GetGames();
            //TODO - improve performance with list of available lobbies
            do
            {
                newID = random.Next(0, MAX_LOBBIES_COUNT);
                lock (_GamesLock)
                {
                    newIdFound = !games.Any(g => g.Id == newID.ToString());
                }

            } while (!newIdFound);

            return newID.ToString();
        }
    }
}