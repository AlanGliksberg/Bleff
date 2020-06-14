using Bleff.CustomExtensions;
using Bleff.Helpers;
using Bleff.Models;
using Bleff.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bleff.Controllers
{
    public class GameController : CustomController
    {
        public ActionResult Join_Game_Init()
        {
            var game = GamesHelper.GetGame();
            var player = GetCurrentPlayer();

            if (game == null)
            {
                player.IsGameLider = true;
                game = GamesHelper.CreateNewGame(player);
            }
            else
            {
                if (game.State == GameState.Started)
                {
                    TempData[Keys.TempDataKeys.GameStarted] = true;
                    return RedirectToAction("index", "home");
                }

                GamesHelper.AddPlayerToGame(game, player);
            }

            var gameVM = new GameVM();
            gameVM.ActualGame = game;
            gameVM.ActualPlayer = player;

            Session.Set(Keys.GameKeys.ActualGame, gameVM);

            return RedirectToAction("waiting-game");
        }

        public ActionResult Waiting_Game()
        {
            var gameVM = Session.Get<GameVM>(Keys.GameKeys.ActualGame);

            if (gameVM == null)
                return RedirectToAction("index", "home");

            return View(gameVM);
        }

        public ActionResult Start_Game()
        {
            var gameVM = Session.Get<GameVM>(Keys.GameKeys.ActualGame);
            GamesHelper.StartGame();

            return RedirectToAction("play");
        }

        public ActionResult Play()
        {
            var gameVM = Session.Get<GameVM>(Keys.GameKeys.ActualGame);

            if (gameVM == null)
                return RedirectToAction("index", "home");

            return View(gameVM);
        }

        public async Task<JsonResult> GetRandomWord()
        {
            var response = await Helpers.ApiHelper.Get("http://www.poxet.com.ar/bleffonardo/palabra.php");

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}