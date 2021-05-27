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
        public ActionResult Create(KhuyenMai khuyenMai)
        {
            CheckThongTinCreate(khuyenMai);
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

            // Loại khuyến mãi và mức giảm giá
            if (khuyenMai.Mucgiam == null)
            {
                ModelState.AddModelError("Mucgiam", "Mức giảm giá không được rỗng.");
            }
            else
            {
                if (khuyenMai.Maloai.ToString() == "KML-02")
                {
                    if(khuyenMai.Mucgiam < 1 || khuyenMai.Mucgiam > 100)
                    {
                        ModelState.AddModelError("Mucgiam", "Mức giảm giá theo % có giá trị từ 1-100%");
                    }
                }
                else
                {
                    if(khuyenMai.Mucgiam < 1)
                    {
                        ModelState.AddModelError("Mucgiam", "Mức giảm giá phải lớn hơn 1");
                    }
                }
            }

            // Loại áp dụng và giá trị tối thiểu
            if (khuyenMai.MaApDung.ToString() == "KMAD-01")
            {
            }
            else
            {
                if(khuyenMai.GTapdung <1)
                {
                    ModelState.AddModelError("GTapdung", "Giá trị áp dụng phải lớn hơn 1");
                }
                else if(khuyenMai.GTapdung < khuyenMai.Mucgiam)
                {
                    ModelState.AddModelError("GTapdung", "Giá trị áp dụng phải lớn hơn mức giảm giá");
                }
                else if(khuyenMai.GTapdung == null)
                {
                    ModelState.AddModelError("GTapdung", "Giá trị áp dụng không được bỏ trống");
                }
                else { }
            }

            // Thời gian có event
            if(khuyenMai.TGbatdau == null)
            {
                ModelState.AddModelError("TGbatdau", "Thời gian bắt đầu khuyến mãi không được trống");
            }
            else if(khuyenMai.TGketthuc == null)
            {
                ModelState.AddModelError("TGketthuc", "Thời gian kết thúc khuyến mãi không được trống");
            }
            else
            {
                string[] time1 = khuyenMai.TGbatdau.ToString().Split('/');
                string[] year1 = time1[2].Split(' ');
                string[] time2 = khuyenMai.TGketthuc.ToString().Split('/');
                string[] year2 = time2[2].Split(' ');
                DateTime date1 = new DateTime(int.Parse(year1[0]), int.Parse(time1[0]),int.Parse(time1[1]),0,0,0);
                DateTime date2 = new DateTime(int.Parse(year2[0]), int.Parse(time2[0]), int.Parse(time2[1]), 0, 0, 0);
                int result = DateTime.Compare(date1, date2);
                string relationship;

                if (result < 0) { }
                else if (result == 0)
                    ModelState.AddModelError("TGketthuc", "Thời gian kết thúc khuyến mãi không trùng với thời gian bắt đầu");
                else
                    ModelState.AddModelError("TGketthuc", "Thời gian kết thúc khuyến mãi sau thời gian bắt đầu");

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
            KhuyenMai khuyenMai = db.KhuyenMais.SingleOrDefault(m => m.Makhuyenmai == id);
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
            KhuyenMai khuyenMai = db.KhuyenMais.SingleOrDefault(m => m.Makhuyenmai == id);
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
