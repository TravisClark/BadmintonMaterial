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
        public CT25Team17Entities db = new CT25Team17Entities();
        [Authorize(Roles = "Admin")]

        // GET: DonHang
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.KhachHang).Include(d => d.SanPham).Include(d => d.TrangThaiDonHang).Include(d => d.KhuyenMai);
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status");
            ViewBag.hinh = ".png";
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
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name");
            ViewBag.product_id = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status");
            ViewBag.voucher = new SelectList(db.KhuyenMais, "Makhuyenmai", "Makhuyenmai");
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,quantity,customer_id,date,status,voucher,price")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.customer_id);
            ViewBag.product_id = new SelectList(db.SanPhams, "MaSP", "TenSP", donHang.product_id);
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
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.customer_id);
            ViewBag.product_id = new SelectList(db.SanPhams, "MaSP", "TenSP", donHang.product_id);
            ViewBag.status = new SelectList(db.TrangThaiDonHangs, "status", "name_status", donHang.status);
            ViewBag.voucher = new SelectList(db.KhuyenMais, "Makhuyenmai", "Maloai", donHang.voucher);
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,quantity,customer_id,date,status,voucher,price")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.customer_id);
            ViewBag.product_id = new SelectList(db.SanPhams, "MaSP", "TenSP", donHang.product_id);
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
