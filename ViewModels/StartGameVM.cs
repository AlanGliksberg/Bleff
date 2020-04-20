using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.ViewModels
{
    public class StartGameVM
    {
        public StartingAction StartedActionSelected { get; set; }
        public int GameId { get; set; }
        public string PlayerName { get; set; }
    }
}