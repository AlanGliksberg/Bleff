using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.ViewModels
{
    public class JoinGameVM : iStartGame
    {
        public int GameId { get; set; }
        public StartingAction StartedActionSelected { get; set; }
        public string PlayerName { get; set; }
    }
}