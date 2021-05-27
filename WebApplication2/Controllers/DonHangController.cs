using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DonHangController : Controller
    {
        private CT25Team17Entities db = new CT25Team17Entities();

        // GET: DonHang
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.ChiTietDonHang).Include(d => d.KhachHang).Include(d => d.TrangThaiDonHang).Include(d => d.KhuyenMai);
            return View(donHangs.ToList());
        }

        // GET: DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DonHang/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.ChiTietDonHangs, "order_id", "product_id");
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name");
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status");
            ViewBag.voucher = new SelectList(db.KhuyenMais, "Makhuyenmai", "Maloai");
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,order_id,customer_id,date,status,voucher")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.ChiTietDonHangs, "order_id", "product_id", donHang.order_id);
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.customer_id);
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status", donHang.status);
            ViewBag.voucher = new SelectList(db.KhuyenMais, "Makhuyenmai", "Maloai", donHang.voucher);
            return View(donHang);
        }

        // GET: DonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.ChiTietDonHangs, "order_id", "product_id", donHang.order_id);
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.customer_id);
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status", donHang.status);
            ViewBag.voucher = new SelectList(db.KhuyenMais, "Makhuyenmai", "Maloai", donHang.voucher);
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,order_id,customer_id,date,status,voucher")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.ChiTietDonHangs, "order_id", "product_id", donHang.order_id);
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.customer_id);
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status", donHang.status);
            ViewBag.voucher = new SelectList(db.KhuyenMais, "Makhuyenmai", "Maloai", donHang.voucher);
            return View(donHang);
        }

        // GET: DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
