using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.Helpers
{
    public static class GamesHelper
    {
        private const int MAX_LOBBIES_COUNT = 100;
        private static Game _Game { get; set; }

        private static object _GamesLock = new object();


        public static Game CreateNewGame(Player player)
        {
            lock (_GamesLock)
            {
                _Game = new Game(player);
            }

            return _Game;
        }

        public static Game GetGame()
        {
            return _Game;
        }

        public static void AddPlayerToGame(Game game, Player player)
        {
            lock (_GamesLock)
            {
                game.Players.Add(player);
            }
        }

        //public static void RemovePlayerFromGame(string playerID, string lobbyID)
        //{
        //    var games = _GetGames();
        //    lock (_GamesLock)
        //    {
        //        var game = games.FirstOrDefault(l => l.Id == lobbyID);
        //        game.Players.RemoveAll(p => p.PlayerID == playerID);
        //        if (game.Players.Count == 0) games.Remove(game);
        //    }
        //}

        //public static void MakeNewLider(string playerID, string lobbyID)
        //{
        //    var games = _GetGames();
        //    lock (_GamesLock)
        //    {
        //        games.FirstOrDefault(g => g.Id == lobbyID).Players.FirstOrDefault(p => p.PlayerID == playerID).GameLider = true;
        //    }
        //}

        //public static void SetSelectedWord(string lobbyID, string playerID, string word, string definition)
        //{
        //    var games = _GetGames();
        //    lock (_GamesLock)
        //    {
        //        var game = games.FirstOrDefault(g => g.Id == lobbyID);
        //        game.SelectedWord = word;
        //        game.SelectedDefinition = definition;
        //        game.PlayersDefinitions = new Dictionary<string, string>();
        //        game.PlayersDefinitions.Add(playerID, definition);
        //    }
        //}

        ///// <summary>
        ///// </summary>
        ///// <returns>true if all players submitted their answer</returns>
        //public static bool SetDefinition(string lobbyID, string playerID, string definition)
        //{
        //    var games = _GetGames();
        //    lock (_GamesLock)
        //    {
        //        var game = games.FirstOrDefault(g => g.Id == lobbyID);
        //        game.PlayersDefinitions.Add(playerID, definition);

        //        if (game.Players.Count == game.PlayersDefinitions.Count)
        //            return true;
        //    }

        //    return false;
        //}

        public static void StartGame()
        {
            var game = GetGame();
            lock (_GamesLock)
            {
                game.State = GameState.Started;
                game.ActualCoordinator = game.Players.FirstOrDefault(p => p.IsGameLider);
            }
        }
    }
}