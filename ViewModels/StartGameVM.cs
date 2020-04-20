using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleff.ViewModels
{
    public class StartGameVM
    {
        public StartingAction StartedActionSelected { get; set; }
        public string PlayerName { get; set; }
        public int GameId { get; set; }
    }
}
