﻿using Bleff.Models;
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

        public static void RemovePlayerFromGame(string playerID)
        {
            lock (_GamesLock)
            {
                var game = _Game;
                game.Players.RemoveAll(p => p.PlayerID == playerID);
                if (game.Players.Count == 0) _Game = null;
            }
        }

        public static void MakeNewLider(string playerID)
        {
            lock (_GamesLock)
            {
                var newLider = _Game.Players.FirstOrDefault(p => p.PlayerID == playerID);
                newLider.IsGameLider = true;
                _Game.ActualCoordinator = newLider;
            }
        }

        public static void SetSelectedWord(string playerID, string word, string definition)
        {
            lock (_GamesLock)
            {
                var game = _Game;
                game.SelectedWord = word;
                game.SelectedDefinition = definition;
                game.PlayersDefinitions = new Dictionary<string, string>();
                game.PlayersDefinitions.Add(playerID, definition);
            }
        }

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

        public static List<int> AllIndexesOf(this string str, string value) {
    if (String.IsNullOrEmpty(value))
        throw new ArgumentException("the string to find may not be empty", "value");
    List<int> indexes = new List<int>();
    for (int index = 0;; index += value.Length) {
        index = str.IndexOf(value, index);
        if (index == -1)
            return indexes;
        indexes.Add(index);
    }
}
    }
}