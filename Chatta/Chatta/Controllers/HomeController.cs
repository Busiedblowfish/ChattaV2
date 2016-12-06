using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chatta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Chatta: Encrypted Web Chat";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Contact Us";

            return View();
        }
    }
}