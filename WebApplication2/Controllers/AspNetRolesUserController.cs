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
    public class AspNetRolesUserController : Controller
    {
        private WebsiteEntities db = new WebsiteEntities();

        // GET: AspNetRoles/Create
        public ActionResult Create(string RoleId)
        {
            ViewBag.Role = db.AspNetRoles.Find(RoleId);
            ViewBag.Users = new SelectList(db.AspNetUsers,"Id","UserName");
            return View();
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string RoleID, string userID)
        {
            var role = db.AspNetRoles.Find(RoleID);
            var user = db.AspNetUsers.Find(userID); 

            role.AspNetUsers.Add(user);
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "AspNetRoles");
        }

        // GET: AspNetRoles/Delete/5
        public ActionResult Delete(string roleId, string userId)
        {
            var role = db.AspNetRoles.Find(roleId);
            var user = db.AspNetUsers.Find(userId);

            role.AspNetUsers.Remove(user);
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "AspNetRoles");
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
