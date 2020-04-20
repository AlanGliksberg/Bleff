using Bleff.CustomExtensions;
using Bleff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bleff.Controllers
{
    public class CustomController : Controller
    {
        public Player GetCurrentPlayer()
        {
            return Session.Get<Player>(Keys.PlayerKeys.Player);
        }
    }
}