using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NLog;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private StudentEntities1 db = new StudentEntities1();


        // GET: Course
        /// <summary>
        /// this the Index Method for Desplay the list of Courses
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    return View(db.courses.ToList());
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Course/Details
        /// <summary>
        /// this is for get the details about Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                course course = db.courses.FirstOrDefault(x => x.id == id);
                return View(course);
            }
            catch (Exception ex)
            {
                return View(new course());
            }
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        /// <summary>
        /// this is for Create a new Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,course1,duration")] course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return View(course);
        }

        // GET: Course/Edit

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Edit
        /// <summary>
        /// this is for Update a Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,course1,duration")] course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View(course);
        }

        // GET: Course/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete
        /// <summary>
        /// this is for Delete the Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                List<registration> reg = db.registrations.ToList();
                if (reg != null)
                {
                    var result = reg.ToList().First(x => x.course_id == id);
                    if (result != null)
                    {

                        ViewBag.Message = "xyzabcd";
                        ViewBag.AlertMsg = "Student Enrollerd in this Course";

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    }
                    else
                    {
                        course course = db.courses.Find(id);
                        db.courses.Remove(course);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    course course = db.courses.Find(id);
                    db.courses.Remove(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex, "Course");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
