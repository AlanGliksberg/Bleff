using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bleff.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Start_Game_Init()
        {
            return RedirectToAction("waiting-game");
        }

        public ActionResult Waiting_Game()
        {
            return View();
        }
    }
}