using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.Models
{
    public class Game
    {
        public Game(Player liderPlayer)
        {
            Id = Helpers.GamesHelper.GenerateGameID();
            Players = new List<Player>()
            {
                liderPlayer
            };
        }

        public object _PlayersLock = new object();

        public GameState State { get; set; }
        public int Id { get; set; }
        public List<Player> Players { get; set; }
    }
}