using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NLog;
using StudentManagement.Common;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private StudentEntities1 db = new StudentEntities1();

        // GET: User
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View(db.users.ToList());
            }
            else
            {
                return RedirectToAction("About", "Home");
            }

        }

        // GET: User
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }*/

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// this is for Create A Admin and also for ncryptPassword logic
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "id,firstname,lastname,username,password")] user user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.users.Where(u => u.username == user.username).Any())
                    {
                        ModelState.AddModelError("", "User Name is Already Taken, try with another User Name");
                    }
                    else
                    {
                        Password encryptPassword = new Password();
                        user.password = encryptPassword.EncryptPassword(user.password);
                        db.users.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Login");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(user);
        }

        // GET: User/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,username,password")] user user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: User/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
