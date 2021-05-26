using System;
using System.Collections.Generic;
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
    public class SanPhamController : Controller
    {
        public CT25Team17Entities db = new CT25Team17Entities();
        // GET: SanPham

        public ActionResult Index(string returnUrl)
        {
            var sp = db.SanPhams.ToList();
            ViewBag.hinh = ".png";

            return View(sp);

        }

        public ActionResult Search(String Search)
        {
            var sp = db.SanPhams.Where(c => c.TenSP.ToLower().Contains(Search)).ToList();


            ViewBag.search = Search;
            ViewBag.hinh = ".png";

            return View(sp);
        }
        public ActionResult ChiTietSanPham(String maSp)
        {
            var sp = db.SanPhams.Where(c => c.MaSP == maSp).First();
            ViewBag.hinh = maSp + ".png";
            return View(sp);
        }


    }
}