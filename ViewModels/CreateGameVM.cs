using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.ViewModels
{
    public class CreateGameVM : iStartGame
    {
        public StartingAction StartedActionSelected { get; set; }
        public string PlayerName { get; set; }
    }
}