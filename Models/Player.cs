using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.Models
{
    public class Player
    {
        public Player(string name)
        {
            PlayerID = Guid.NewGuid().ToString("N");
            Name = name;
            IsGameLider = false;
        }
        public Player(string name, bool gameLider)
        {
            PlayerID = Guid.NewGuid().ToString("N");
            Name = name;
            IsGameLider = gameLider;
        }

        public string PlayerID { get; set; }
        public string Name { get; set; }
        public bool IsGameLider { get; set; }
    }
}