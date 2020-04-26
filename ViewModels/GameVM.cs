using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.ViewModels
{
    public class GameVM
    {
        public bool CheckedIn { get; set; }
        public Game ActualGame { get; set; }
        public Player ActualPlayer { get; set; }
    }
}