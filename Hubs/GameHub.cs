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
        /// <summary>
        /// Key: connectionID
        /// Pair: HubPlayer (PlayerID, LobbyID)
        /// </summary>
        public static Dictionary<string, HubPlayer> _PlayersConnected { get; set; }

        private static object _LockPlayers = new object();

        public void _AddPlayer(string connectionId, string playerID, string lobbyID)
        {
            lock (_LockPlayers)
            {
                if (_PlayersConnected == null) _PlayersConnected = new Dictionary<string, HubPlayer>();
                if (!_PlayersConnected.Any(p => p.Value.PlayerID == playerID))
                    _PlayersConnected.Add(connectionId, new HubPlayer()
                    {
                        LobbyID = lobbyID,
                        PlayerID = playerID
                    });
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
        public void LoadLobbyData(string lobbyID, string playerID)
        {
            _AddPlayer(Context.ConnectionId, playerID, lobbyID);
            Groups.Add(Context.ConnectionId, lobbyID);
        }

        public void StartRound(string word, string definition)
        {
            var lobby = _GetLobby(Context.ConnectionId);
            Helpers.GamesHelper.SetSelectWord(lobby, word, definition);
            Clients.Group(lobby).StartRound(word);
        }
    }
}