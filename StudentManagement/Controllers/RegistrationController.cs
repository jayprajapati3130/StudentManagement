using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NLog;
using Org.BouncyCastle.Crypto;
using Rotativa;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private StudentEntities1 db = new StudentEntities1();


        // GET: Registration
        /// <summary>
        /// this is for get the List of Students
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()  // string Searchby, string search
        {
            //if (Session["UserId"] != null)
            // {
            /* var registrations = db.registrations.Include(r => r.batch).Include(r => r.course);
             try
             {
                 if (Searchby == "firstname")
                 {
                     var model = db.registrations.Where(s => s.firstname.StartsWith(search) || search == null).ToList();
                     return View(model);

                 }
                 else if (Searchby == "course_id")
                 {
                     var statuses = db.courses.ToList().Where(x => x.course1 == search).FirstOrDefault();
                     int Code = statuses.id;
                     var model = db.registrations.Where(s => s.course_id == Code || search == null).ToList();
                     return View(model);
                 }
             }
             catch (Exception ex)
             {
                 logger.Error(ex, "Register");
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }*/

            return View(db.registrations.ToList());

            /* }
             else
             {

                 return RedirectToAction("About", "Home");
             }*/
        }

        public ActionResult PrintPDF()
        {
            var result = db.registrations.ToList();
            return new ActionAsPdf("Index", result);
        }
        /// <summary>
        /// this is for Generate the Report
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintReport()
        {
            var registrations = db.registrations.ToList();
            // return View(registrations.ToList());
            return new ViewAsPdf("PrintReport", registrations)
            {
                FileName = "PrintReport.pdf",

                MinimumFontSize = 14,
                PageMargins = { Left = 20, Right = 20 },

            };
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.courses, "id", "course1");
            return View();
        }

        // POST: Registration/Create
        /// <summary>
        /// this is for Create a New Student
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,course_id,batch_id,telno")] registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    registration.created_by = Session["UserName"].ToString();
                    registration.create_time = DateTime.Now;
                    db.registrations.Add(registration);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registration.batch_id);
                ViewBag.course_id = new SelectList(db.courses, "id", "course1", registration.course_id);
                return View(registration);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Registration/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registration.course_id);
            return View(registration);
        }

        // POST: Registration/Edit/5
        /// <summary>
        /// this is for Update the student Details
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,course_id,batch_id,telno,created_by,create_time")] registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var result = db.registrations.Find(id);
                    db.Entry(registration).State = EntityState.Modified;
                    registration.updated_by = Session["UserName"].ToString();
                    registration.update_time = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registration.batch_id);
                ViewBag.course_id = new SelectList(db.courses, "id", "course1", registration.course_id);
                return View(registration);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registration registration = db.registrations.Find(id);
            db.registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /* protected override void Dispose(bool disposing)
         {
             if (disposing)
             {
                 db.Dispose();
             }
             base.Dispose(disposing);
         }*/

    }
}
