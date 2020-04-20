using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bleff.Controllers
{
    public class GameController : CustomController
    {
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


            return RedirectToAction("waiting-game");
        }

        public ActionResult Waiting_Game()
        {
            return View();
        }
    }
}