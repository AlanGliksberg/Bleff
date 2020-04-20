using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleff.ViewModels
{
    public interface iStartGame
    {
        StartingAction StartedActionSelected { get; set; }
        string PlayerName { get; set; }
    }
}
