using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NLog;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class BatchController : Controller
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private StudentEntities1 db = new StudentEntities1();

        // GET: Batch
        /// <summary>
        /// This is the Index Method to Get the List of Batchs
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View(db.batches.ToList());
            }
            else
            {
                return RedirectToAction("About", "Home");
            }
        }

        // GET: Batch/Details
        /// <summary>
        /// this is for desplay the Entire Information 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch batch = db.batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // GET: Batch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Batch/Create
        /// <summary>
        /// this is for Create a New Batch
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,batch1,year")] batch batch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.batches.Add(batch);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return View(batch);
        }

        // GET: Batch/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch batch = db.batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Batch/Edit/
        /// <summary>
        /// this is for upadte the batch details
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,batch1,year")] batch batch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(batch).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

            }
            return View(batch);
        }

        // GET: Batch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch batch = db.batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Batch/Delete/5
        /// <summary>
        /// this is for Delete the batch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            batch batch = db.batches.Find(id);
            db.batches.Remove(batch);
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
