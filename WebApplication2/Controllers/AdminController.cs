using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;


namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        public CT25Team17Entities db = new CT25Team17Entities();

        [Authorize(Roles = "Admin")]
        // GET: SanPhams       
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.NhomSanPham);
            return View(sanPhams.ToList());
        }
        public ActionResult Picture(string MaSP)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + MaSP + ".png", "images");
        }

        private const string PICTURE_PATH = "~/images/";
    }
}