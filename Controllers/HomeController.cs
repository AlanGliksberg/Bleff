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
    public class HomeController : CustomController
    {
        [HttpGet]
        public ActionResult Index()
        {
            //validar no encontro game
            //var gameSearchFailed = TempData[Keys.TempDataKeys.GameSearchFailed]
            //var GameStarted = TempData[Keys.TempDataKeys.GameStarted]
            //var GameOver = TempData[Keys.TempDataKeys.GameOver]
            Session.Set(Keys.PlayerKeys.Player, null);

            return View();
        }

        [HttpPost]
        public ActionResult Index(StartGameVM startGameVM)
        {
            var player = new Player(startGameVM.PlayerName);
            Session.Set(Keys.PlayerKeys.Player, player);


            return RedirectToAction("join-game-init", "game", new { gameID = startGameVM.GameId });
        }
    }
}