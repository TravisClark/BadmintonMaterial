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
    [Authorize(Roles = "Admim")]
    public class SanPhamController : Controller
    {
        
        public CT25Team17Entities db = new CT25Team17Entities();
        
        // GET: SanPhams
        public ActionResult IndexAdmin()
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

        public ActionResult Picture(string MaSP)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + MaSP + ".png", "images");
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
            CheckThongTinCreate(model);
            if (ModelState.IsValid)
            {
                if(picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.SanPhams.Add(model);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + model.MaSP + ".png");

                        scope.Complete();
                        return RedirectToAction("IndexAdmin");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("","Chưa chọn ảnh");
                }                  
            }

            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", model.MaNhom);
            return View(model);
        }

        private const string PICTURE_PATH = "~/images/";

        private void CheckThongTinCreate(SanPham sanPham)
        {
            var regexItem = new Regex("^[ a-z A-Z 0-9 ]*$");
            var regexNumeric = new Regex("^[ 0-9 ]*$");
            var regexWord = new Regex("^[ a-z A-Z ]*$");
            //Kiem tra MaSP
            foreach (var item in db.SanPhams)
            {
                if(sanPham.MaSP == item.MaSP)
                {
                    ModelState.AddModelError("MaSP", "Mã sản phẩm đã tồn tại.");
                    break;
                }
            }
            if (sanPham.MaSP == null)
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.MaSP.Length < 5 || sanPham.MaSP.Length >10)
                {
                    ModelState.AddModelError("MaSP", "Mã sản phẩm phải có từ 5 đến 10 ký tự.");
                }
                else
                {
                    if(sanPham.MaSP.IndexOf(" ") >= 0)
                    {
                        ModelState.AddModelError("MaSP", "Mã sản phẩm không chứa khoảng trắng.");
                    }
                    else
                    {
                        if (regexItem.IsMatch(sanPham.MaSP) == false)
                        {
                            ModelState.AddModelError("MaSP", "Mã sản phẩm chỉ chứa ký tự alphabet(a-z,A-Z,0-9).");
                        }
                    }
                }
            }
            //Kiem tra TenSP           
            if (sanPham.TenSP == null)
            {
                ModelState.AddModelError("TenSP", "Tên sản phẩm không được để trống.");
            }
            else
            {
                if(sanPham.TenSP.IndexOf(" ") == 0 || regexWord.IsMatch(sanPham.TenSP.FirstOrDefault().ToString()) == false)
                {
                    ModelState.AddModelError("TenSP", "Tên sản phẩm phải có ký tự đầu tiên là chữ.");
                }
                else
                {
                    if (sanPham.TenSP.Length > 50)
                    {
                        ModelState.AddModelError("TenSP", "Tên sản phẩm phải dưới 50 ký tự.");
                    }
                }
            }

            //Kiem tra ThuongHieu
            if (sanPham.ThuongHieu == null)
            {
                ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.ThuongHieu.IndexOf(" ") == 0 || regexWord.IsMatch(sanPham.ThuongHieu.FirstOrDefault().ToString()) == false)
                {
                    ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm phải có ký tự đầu tiên là chữ.");
                }
                else
                {
                    if (sanPham.ThuongHieu.Length > 50)
                    {
                        ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm phải dưới 50 ký tự.");
                    }
                    else
                    {
                        if(regexNumeric.IsMatch(sanPham.ThuongHieu) == true || regexWord.IsMatch(sanPham.ThuongHieu) == false)
                        {
                            ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm chỉ chứa ký tự chữ cái.");
                        }
                    }
                }
            }
            //Kiem tra MaNhom
            if (sanPham.MaNhom == null)
            {
                ModelState.AddModelError("MaNhom", "Nhóm sản phẩm không được để trống.");
            }
            //Kiem tra MoTa
            if (sanPham.MoTa == null)
            {
                ModelState.AddModelError("MoTa", "Mô tả sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.MoTa.IndexOf(" ") == 0 || regexWord.IsMatch(sanPham.MoTa.FirstOrDefault().ToString()) == false)
                {
                    ModelState.AddModelError("MoTa", "Mô tả sản phẩm phải có ký tự đầu tiên là chữ.");
                }
            }
            //Kiem tra File anh

            //Kiem tra GiaSP
            if (sanPham.GiaSP == null)
            {
                ModelState.AddModelError("GiaSP", "Giá sản phẩm không được để trống.");
            }
            else
            {
                if(sanPham.GiaSP <= 0)
                {
                    ModelState.AddModelError("GiaSP", "Giá sản phẩm phải lớn hơn 0đ.");
                }               
            }
            //Kiem tra SoLuong
            if (sanPham.SoLuong == null)
            {
                ModelState.AddModelError("SoLuong", "Số lượng sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.SoLuong <= 0)
                {
                    ModelState.AddModelError("SoLuong", "Số lượng sản phẩm phải lớn hơn 0đ.");
                }
            }
        }

        private void CheckThongTinEdit(SanPham sanPham)
        {
            var regexItem = new Regex("^[ a-z A-Z 0-9 ]*$");
            var regexNumeric = new Regex("^[ 0-9 ]*$");
            var regexWord = new Regex("^[ a-z A-Z ]*$");           
            //Kiem tra TenSP           
            if (sanPham.TenSP == null)
            {
                ModelState.AddModelError("TenSP", "Tên sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.TenSP.IndexOf(" ") == 0 || regexWord.IsMatch(sanPham.TenSP.FirstOrDefault().ToString()) == false)
                {
                    ModelState.AddModelError("TenSP", "Tên sản phẩm phải có ký tự đầu tiên là chữ.");
                }
                else
                {
                    if (sanPham.TenSP.Length > 100)
                    {
                        ModelState.AddModelError("TenSP", "Tên sản phẩm phải dưới 100 ký tự.");
                    }
                }
            }
            //Kiem tra ThuongHieu
            if (sanPham.ThuongHieu == null)
            {
                ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.ThuongHieu.IndexOf(" ") == 0 || regexWord.IsMatch(sanPham.ThuongHieu.FirstOrDefault().ToString()) == false)
                {
                    ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm phải có ký tự đầu tiên là chữ.");
                }
                else
                {
                    if (sanPham.ThuongHieu.Length > 100)
                    {
                        ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm phải dưới 100 ký tự.");
                    }
                    else
                    {
                        if (regexNumeric.IsMatch(sanPham.ThuongHieu) == true || regexWord.IsMatch(sanPham.ThuongHieu) == false)
                        {
                            ModelState.AddModelError("ThuongHieu", "Thương hiệu sản phẩm chỉ chứa ký tự chữ cái.");
                        }
                    }
                }
            }
            //Kiem tra MaNhom
            if (sanPham.MaNhom == null)
            {
                ModelState.AddModelError("MaNhom", "Nhóm sản phẩm không được để trống.");
            }
            //Kiem tra MoTa
            if (sanPham.MoTa == null)
            {
                ModelState.AddModelError("MoTa", "Mô tả sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.MoTa.IndexOf(" ") == 0 || regexWord.IsMatch(sanPham.MoTa.FirstOrDefault().ToString()) == false)
                {
                    ModelState.AddModelError("MoTa", "Mô tả sản phẩm phải có ký tự đầu tiên là chữ.");
                }
            }
            //Kiem tra File anh

            //Kiem tra GiaSP
            if (sanPham.GiaSP == null)
            {
                ModelState.AddModelError("GiaSP", "Giá sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.GiaSP <= 0)
                {
                    ModelState.AddModelError("GiaSP", "Giá sản phẩm phải lớn hơn 0đ.");
                }
            }
            //Kiem tra SoLuong
            if (sanPham.SoLuong == null)
            {
                ModelState.AddModelError("SoLuong", "Số lượng sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.SoLuong <= 0)
                {
                    ModelState.AddModelError("SoLuong", "Số lượng sản phẩm phải lớn hơn 0đ.");
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
        public ActionResult Edit(SanPham model, HttpPostedFileBase picture)
        {
            CheckThongTinEdit(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    if(picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + model.MaSP + ".png");
                    }

                    scope.Complete();
                    return RedirectToAction("IndexAdmin");
                }                                       
            }
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", model.MaNhom);
            return View(model);
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
            return RedirectToAction("IndexAdmin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        [AllowAnonymous]
        // GET: SanPhamGiaoDienKhachHang
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