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

        private static List<Player> _GetGamePlayers(Game game)
        {
            return game.Players;
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
                throw new Exception("Lobbies are full");

            var newGame = new Game(player);
            _AddGameToList(newGame);

            return newGame;
        }

        public static Game GetGameByID(int id)
        {
            return _GetGames().FirstOrDefault(g => g.Id == id);
        }

        public static void AddPlayerToGame(Game game, Player player)
        {
            lock (_GamesLock)
            {
                var players = _GetGamePlayers(game);
                players.Add(player);
            }
        }

        public static int GenerateGameID()
        {
            var random = new Random();
            int newID;
            bool newIdFound = false;

            //TODO - improve performance
            do
            {
                newID = random.Next(0, MAX_LOBBIES_COUNT);
                var games = _GetGames();
                lock (_GamesLock)
                {
                    newIdFound = !games.Any(g => g.Id == newID);
                }

            } while (!newIdFound);

            return newID;
        }
    }
}