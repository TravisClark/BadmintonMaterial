using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    
    public class HoaDonKhachHangController : Controller
    {
        private List<ChiTietGioHang> cart = Session["cart"] as List<ChiTietGioHang>;
        // GET: HoaDonKhachHang
        public ActionResult Index()
        {
            var Session = System.Web.HttpContext.Current.Session;
            cart = Session["cart"] as List<ChiTietGioHang>;
            return View();
        }

        
    }
    
}