using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SanPhamsController : Controller
    {
        private CT25Team17Entities db = new CT25Team17Entities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.NhomSanPham);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        public ActionResult Picture(string id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham model,HttpPostedFileBase picture)
        {
            CheckThongTin(model);
            if (ModelState.IsValid)
            {
                if(picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.SanPhams.Add(model);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + model.MaSP);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("","Chua chon anh");
                }
                
            }

            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", model.MaNhom);
            return View(model);
        }

        private const string PICTURE_PATH = "~/images/";

        private void CheckThongTin(SanPham sanPham)
        {       
            if(sanPham.MaSP == null)
            {
                ModelState.AddModelError("MaSP", "Ma san pham khong duoc bo trong.");
            }
            else
            {
                if (sanPham.MaSP.Length < 5 || sanPham.MaSP.Length >10)
                {
                    ModelState.AddModelError("MaSP", "Ma san pham phai tu 5 den 10 ky tu.");
                }
            }
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", sanPham.MaNhom);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,ThuongHieu,MaNhom,MoTa,GiaSP,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", sanPham.MaNhom);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
