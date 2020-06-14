using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bleff.Models;
using Microsoft.AspNet.SignalR;

namespace Bleff.Hubs
{
    public class GameHub : Hub
    {
        public void RemovePlayer(string playerID)
        {
            Helpers.GamesHelper.RemovePlayerFromGame(playerID);
            Clients.All.RemovePlayer(playerID);
        }

        public void StartRound(string playerID, string word, string definition)
        {
            if (String.IsNullOrEmpty(word) || String.IsNullOrEmpty(definition)) return;

            Helpers.GamesHelper.SetSelectedWord(playerID, word, definition);
            Clients.All.StartRound(word);
        }

        //public void SubmitDefinition(string definition)
        //{
        //    var player = _GetPlayer(Context.ConnectionId);

        //    if (Helpers.GamesHelper.SetDefinition(player.LobbyID, player.PlayerID, definition))
        //    {
        //        var game = Helpers.GamesHelper.GetGameByID(player.LobbyID);
        //        var definitions = game.PlayersDefinitions.Select(d => d.Value).ToList();
        //        Clients.Group(player.LobbyID).CheckDefinitions(definitions);
        //    }
        //}
    }
}