using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.Helpers
{
    public static class GamesHelper
    {
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

        private static List<Player> _GetGamePlayers(Game game)
        {
            return game.Players;
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

            do
            {
                newID = random.Next(0, 100);
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