using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommInfo.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A site about the small town of Aiea.";

            return View();
        }

        public ActionResult History()
        {
            ViewBag.Message = "A short history about Aiea.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Got questions? Here's how to get in touch with us:";

            return View();
        }
    }
}