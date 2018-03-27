using System;
using System.Linq;
using System.Web.Mvc;

namespace Selfless.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Desenvolvedor()
        {
            return View("Desenvolvedor");
        }

    }
}
