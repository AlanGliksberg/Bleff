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
            if (game.Players == null)
            {
                lock (game._PlayersLock)
                {
                    if (game.Players == null)
                    {
                        game.Players = new List<Player>();
                    }
                }
            }
            return game.Players;
        }

        public static Game GetGameByID(int id)
        {
            return _GetGames().FirstOrDefault(g => g.Id == id);
        }

        public static void AddPlayerToGame(Game game, Player player)
        {
            var players = _GetGamePlayers(game);
            players.Add(player);
        }
    }
}