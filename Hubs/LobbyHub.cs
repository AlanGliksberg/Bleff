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
        /// <summary>
        /// Key: connectionID
        /// Pair: HubPlayer (PlayerID, LobbyID)
        /// </summary>
        private static Dictionary<string, HubPlayer> _PlayersConnected { get; set; }

        private void _AddPlayer(string connectionId, string playerID, string lobbyID)
        {
            if (_PlayersConnected == null) _PlayersConnected = new Dictionary<string, HubPlayer>();
            if (!_PlayersConnected.Any(p => p.Value.PlayerID == playerID))
                _PlayersConnected.Add(connectionId, new HubPlayer()
                {
                    LobbyID = lobbyID,
                    PlayerID = playerID
                });
        }
        private HubPlayer _GetPlayer(string connectionId)
        {
            return _PlayersConnected.FirstOrDefault(p => p.Key == connectionId).Value;
        }
        private void _RemovePlayer(string connectionId)
        {
            _PlayersConnected.Remove(connectionId);
        }

        public void AddPlayer(string playerName, string playerID, string lobbyID)
        {
            _AddPlayer(Context.ConnectionId, playerID, lobbyID);
            Groups.Add(Context.ConnectionId, lobbyID);
            Clients.Group(lobbyID).AddPlayer(playerID, playerName);
        }

        public void RemovePlayer()
        {
            var player = _GetPlayer(Context.ConnectionId);
            _RemovePlayer(Context.ConnectionId);
            if (player != null)
            {
                Helpers.GamesHelper.RemovePlayerFromGame(player.PlayerID, player.LobbyID);
                Clients.Group(player.LobbyID).RemovePlayer(player.PlayerID);
            }
        }

        public void NewLider(string playerID, string lobbyID)
        {
            Helpers.GamesHelper.MakeNewLider(playerID, lobbyID);
        }

    }
}