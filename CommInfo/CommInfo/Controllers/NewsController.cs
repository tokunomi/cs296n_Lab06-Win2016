using CommInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommInfo.Controllers
{
    public class NewsController : Controller
    {
        NMonth nmonth = new NMonth();
        public NewsController()
        {
            NDay day001 = new NDay { NewsDay = "NDS001" };  // NDS = NewsDay seed
            News sugarMill = new News { Title = "Aiea Sugar Mill Remembered", Story = "The Aiea Sugar Mill played a big part in the history of Aiea Town." };
            News farmersMarket = new News { Title = "What's new at the Pearlridge Farmer's Market", Story = "If you haven't been to the Farmer's Market, now is the time to visit." };
            day001.News.Add(sugarMill);
            day001.News.Add(farmersMarket);
            nmonth.NDays.Add(day001);
        }
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Today()
        {
            ViewBag.Heading = "Today's News";
            List<News> news = nmonth.NDays[0].News;
            return View(news);
        }

        public ActionResult Archive()
        {
            ViewBag.Heading = "News Archive";
            return View();
        }
    }
}