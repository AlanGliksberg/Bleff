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
            Name = name;
            GameLider = false;
        }
        public Player(string name, bool gameLider)
        {
            Name = name;
            GameLider = gameLider;
        }

        public string Name { get; set; }
        public bool GameLider { get; set; }
    }
}