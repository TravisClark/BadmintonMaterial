using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public CT25Team17Entities db = new CT25Team17Entities();
        public ActionResult Index()
        {
            var sp = db.SanPhams.ToList().Take(4);
            ViewBag.hinh = ".png";


            return View(sp);
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