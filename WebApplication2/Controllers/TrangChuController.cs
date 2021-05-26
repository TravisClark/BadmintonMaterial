using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebApplication2.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu

        public ActionResult Index()
        {
            return View();
        }
        
    }
}