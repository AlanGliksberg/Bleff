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
        /// Value: HubPlayer (PlayerID, LobbyID)
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

        public void StartRound(string word, string definition)
        {
            var player = _GetPlayer(Context.ConnectionId);
            Helpers.GamesHelper.SetSelectedWord(player.LobbyID, player.PlayerID, word, definition);
            Clients.Group(player.LobbyID).StartRound(word);
        }

        public void SubmitDefinition(string definition)
        {
            var player = _GetPlayer(Context.ConnectionId);

            if (Helpers.GamesHelper.SetDefinition(player.LobbyID, player.PlayerID, definition))
            {
                var game = Helpers.GamesHelper.GetGameByID(player.LobbyID);
                var definitions = game.PlayersDefinitions.Select(d => d.Value).ToList();
                Clients.Group(player.LobbyID).CheckDefinitions(definitions);
            }
        }
    }
}