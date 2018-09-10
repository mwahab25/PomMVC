﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PomMVC.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "CUBE POM Transacion";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CUBE";

            return View();
        }
    }
}