using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Bleff.Hubs
{
    public class LobbyHub : Hub
    {
        public void AddPlayer(string playerName)
        {
            Clients.All.AddPlayer(playerName);
        }
    }
}