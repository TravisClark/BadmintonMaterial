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
    public class KhuyenMaisController : Controller
    {
        private CT25Team17Entities db = new CT25Team17Entities();

        // GET: KhuyenMais
        public ActionResult Index()
        {
            var khuyenMais = db.KhuyenMais.Include(k => k.LoaiApDung).Include(k => k.LoaiKhuyenMai);
            return View(khuyenMais.ToList());
        }

        // GET: KhuyenMais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // GET: KhuyenMais/Create
        public ActionResult Create()
        {
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung");
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung");
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai");
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai");
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai");
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai");
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai");
            return View();
        }

        // POST: KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Makhuyenmai,Maloai,Mucgiam,MaApDung,GTapdung,TGbatdau,TGketthuc")] KhuyenMai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.KhuyenMais.Add(khuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung", khuyenMai.MaApDung);
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung", khuyenMai.MaApDung);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            return View(khuyenMai);
        }

        private void CheckThongTinCreate(KhuyenMai khuyenMai)
        {
            var regexItem = new Regex("^[ a-z A-Z 0-9 ]*$");
            var regexNumeric = new Regex("^[ 0-9 ]*$");
            var regexWord = new Regex("^[ a-z A-Z ]*$");
            //Kiem tra MaSP
            foreach (var item in db.KhuyenMais)
            {
                if (khuyenMai.Makhuyenmai == item.Makhuyenmai)
                {
                    ModelState.AddModelError("Makhuyenmai", "Mã khuyến mãi đã tồn tại.");
                }
            }
            if (khuyenMai.Makhuyenmai == null)
            {
                ModelState.AddModelError("Makhuyenmai", "Mã khuyến mãi không được để trống.");
            }
            else
            {
                if (khuyenMai.Makhuyenmai.Length != 10)
                {
                    ModelState.AddModelError("Makhuyenmai", "Mã khuyến mãi phải có đúng 10 ký tự.");
                }
                else
                {
                    if (khuyenMai.Makhuyenmai.IndexOf(" ") >= 0)
                    {
                        ModelState.AddModelError("Makhuyenmai", "Mã khuyến mãi không chứa khoảng trắng.");
                    }
                    else
                    {
                        if (regexItem.IsMatch(khuyenMai.Makhuyenmai) == false)
                        {
                            ModelState.AddModelError("Makhuyenmai", "Mã khuyến mãi chỉ chứa ký tự alphabet(a-z,A-Z,0-9).");
                        }
                    }
                }
            }
        }
            // GET: KhuyenMais/Edit/5
        public ActionResult Edit(string id)
            {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung", khuyenMai.MaApDung);
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung", khuyenMai.MaApDung);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            return View(khuyenMai);
        }

        // POST: KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Makhuyenmai,Maloai,Mucgiam,MaApDung,GTapdung,TGbatdau,TGketthuc")] KhuyenMai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung", khuyenMai.MaApDung);
            ViewBag.MaApDung = new SelectList(db.LoaiApDungs, "MaApDung", "TenLApDung", khuyenMai.MaApDung);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            ViewBag.Maloai = new SelectList(db.LoaiKhuyenMais, "Maloai", "Tenloai", khuyenMai.Maloai);
            return View(khuyenMai);
        }

        // GET: KhuyenMais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            db.KhuyenMais.Remove(khuyenMai);
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
