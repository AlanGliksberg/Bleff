using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Bleff.CustomExtensions;
using Bleff.Models;
using Microsoft.AspNet.SignalR;

namespace Bleff.Hubs
{
    public class LobbyHub : Hub
    {
        public void AddPlayer(string playerID, string playerName)
        {
            Clients.Others.AddPlayer(playerID, playerName);
        }

        public void RemovePlayer(string playerID)
        {
            Helpers.GamesHelper.RemovePlayerFromGame(playerID);
            Clients.All.RemovePlayer(playerID);

        }

        public void NewLider(string playerID)
        {
            Helpers.GamesHelper.MakeNewLider(playerID);
        }

        public void StartGame()
        {
            Clients.All.StartGame();
        }
    }
}