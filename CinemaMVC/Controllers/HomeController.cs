using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index","Filmes");
        }

        public ActionResult About()
        {
            return RedirectToAction("Index", "Filmes");
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Index", "Filmes");
        }
    }
}