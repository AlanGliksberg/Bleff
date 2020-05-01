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
        /// Value: HubPlayer (PlayerID, LobbyID)
        /// </summary>
        public static Dictionary<string, HubPlayer> _PlayersConnected { get; set; }

        private static object _LockPlayers = new object();

        public void _AddPlayer(string connectionId, string playerID, string lobbyID)
        {
            lock (_LockPlayers)
            {
                if (_PlayersConnected == null) _PlayersConnected = new Dictionary<string, HubPlayer>();
                KeyValuePair<string, HubPlayer>? player = _PlayersConnected.FirstOrDefault(p => p.Key == connectionId);
                if (player == null)
                    _PlayersConnected.Add(connectionId, new HubPlayer()
                    {
                        LobbyID = lobbyID,
                        PlayerID = playerID
                    });
                else
                    _PlayersConnected[connectionId] = new HubPlayer()
                    {
                        LobbyID = lobbyID,
                        PlayerID = playerID
                    };
            }
        }
        public HubPlayer _GetPlayer(string connectionId)
        {
            lock (_LockPlayers)
            {
                return _PlayersConnected.FirstOrDefault(p => p.Key == connectionId).Value;
            }
        }

        public string _GetLobby(string connectionId)
        {
            lock (_LockPlayers)
            {
                return _GetPlayer(connectionId).LobbyID;
            }
        }

        public void _RemovePlayer(string connectionId)
        {
            lock (_LockPlayers)
            {
                _PlayersConnected.Remove(connectionId);
            }
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

        public void StartGame()
        {
            var lobby = _GetLobby(Context.ConnectionId);
            Clients.Group(lobby).StartGame();
        }
    }
}