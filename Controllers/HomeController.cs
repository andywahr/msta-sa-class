using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassExample1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.HomeIndexModel model = new Models.HomeIndexModel();
            model.RightNow = DateTime.Now;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}