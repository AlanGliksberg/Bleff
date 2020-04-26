using Bleff.CustomExtensions;
using Bleff.Models;
using Bleff.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bleff.Controllers
{
    public class GameController : CustomController
    {
        public ActionResult Create_Game_Init()
        {
            var player = GetCurrentPlayer();
            var newGame = Helpers.GamesHelper.CreateNewGame(player);

            var gameVM = new GameVM();
            gameVM.ActualGame = newGame;
            gameVM.ActualPlayer = player;

            Session.Set(Keys.GameKeys.ActualGame, gameVM);
            return RedirectToAction("waiting-game");
        }

        public ActionResult Join_Game_Init(string gameID)
        {
            var game = Helpers.GamesHelper.GetGameByID(gameID);

            if (game == null)
            {
                TempData[Keys.TempDataKeys.GameSearchFailed] = true;
                return RedirectToAction("index", "home");
            }

            if (game.State == GameState.Started)
            {
                TempData[Keys.TempDataKeys.GameStarted] = true;
                return RedirectToAction("index", "home");
            }

            var player = GetCurrentPlayer();

            Helpers.GamesHelper.AddPlayerToGame(game, player);

            var gameVM = new GameVM();
            gameVM.ActualGame = game;
            gameVM.ActualPlayer = player;

            Session.Set(Keys.GameKeys.ActualGame, gameVM);

            return RedirectToAction("waiting-game");
        }

        public ActionResult Waiting_Game()
        {
            var gameVM = Session.Get<GameVM>(Keys.GameKeys.ActualGame);

            if (gameVM == null || 
                gameVM.CheckedIn ||
                !gameVM.ActualGame.Players.Any(p => p.PlayerID == gameVM.ActualPlayer.PlayerID)) 
                return RedirectToAction("index", "home");

            gameVM.CheckedIn = true;

            return View(gameVM);
        }

        public ActionResult Start_Game()
        {
            var gameVM = Session.Get<GameVM>(Keys.GameKeys.ActualGame);
            gameVM.CheckedIn = false;
            Helpers.GamesHelper.StartGame(gameVM.ActualGame.Id);

            return RedirectToAction("play");
        }

        public ActionResult Play()
        {
            var gameVM = Session.Get<GameVM>(Keys.GameKeys.ActualGame);

            if (gameVM == null ||
                gameVM.CheckedIn ||
                !gameVM.ActualGame.Players.Any(p => p.PlayerID == gameVM.ActualPlayer.PlayerID))
                return RedirectToAction("index", "home");

            gameVM.CheckedIn = true;

            return View(gameVM);
        }
    }
}