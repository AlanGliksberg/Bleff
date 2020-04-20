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
        public ActionResult Create_Game_Init(int gameID)
        {
            var player = GetCurrentPlayer();
            var newGame = new Game(player);

            var gameVM = new GameVM();
            gameVM.ActualGame = newGame;
            gameVM.ActualPlayer = player;

            Session.Set(Keys.GameKeys.ActualGame, gameVM);
            return RedirectToAction("waiting-game");
        }

        public ActionResult Join_Game_Init(int gameID)
        {
            var game = Helpers.GamesHelper.GetGameByID(gameID);

            if (game == null)
            {
                TempData[Keys.TempDataKeys.GameSearchFailed] = true;
                return RedirectToAction("index", "home");
            }

            if (game.State == Models.GameState.Started)
            {
                TempData[Keys.TempDataKeys.GameStarted] = true;
                return RedirectToAction("index", "home");
            }

            if (game.State == Models.GameState.Over)
            {
                TempData[Keys.TempDataKeys.GameOver] = true;
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

            return View(gameVM);
        }
    }
}