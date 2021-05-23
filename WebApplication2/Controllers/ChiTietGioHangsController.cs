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
    public class ChiTietGioHangsController : Controller
    {
        public CT25Team17Entities db = new CT25Team17Entities();
        private List<ChiTietGioHang> cart = null;
        public ChiTietGioHangsController()
        {
            var Session = System.Web.HttpContext.Current.Session;

            if (Session["cart"] != null)
            {
                cart = Session["cart"] as List<ChiTietGioHang>;
            }
            else
            {
                cart = new List<ChiTietGioHang>();
                Session["cart"] = cart;
            }
        }

        // GET: GioHang
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.hinh = ".png";
            return View(cart);
        }

        // GET: ChiTietGioHangs/Create
        [HttpPost]
        public ActionResult Create(string maSP, int soLuong)
        {
            bool flag = true;
            foreach (var item in cart)
            {


                if (maSP == item.SanPham.MaSP)
                {
                    item.SoLuong += soLuong;
                    flag = false;
                }

            }
            if (flag)
            {


                var sp = db.SanPhams.Find(maSP);
                cart.Add(new ChiTietGioHang
                {
                    SanPham = sp,
                    SoLuong = soLuong
                }); ;

            }

            return RedirectToAction("Index");
        }



        // GET: ChiTietGioHangs/Edit/5
        public ActionResult Edit(string maSP, int soLuong)
        {
            foreach (var item in cart)
            {


                if (maSP == item.SanPham.MaSP)
                {
                    item.SoLuong = soLuong;

                }

            }



            return RedirectToAction("Index");
        }



        // GET: ChiTietGioHangs/Delete/5
        public ActionResult Delete(string maSP)
        {

            cart.RemoveAll(s => s.SanPham.MaSP == maSP);
            return RedirectToAction("Index");
        }

        // POST: ChiTietGioHangs/Delete/5


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}
