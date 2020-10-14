using FootballStatistics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FootballStatistics.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Football statistics";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Website that contains Football statistics";

            return View();
        }


    }
}