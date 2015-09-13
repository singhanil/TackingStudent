using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentTracking.Data;

namespace StudentTracking.Web.Controllers
{
    public class PrimaryTagsController : Controller
    {
        private StudentTrackingEntities db = new StudentTrackingEntities();

        // GET: PrimaryTags
        public ActionResult Index()
        {
            return View(db.PrimaryTags.ToList());
        }

        // GET: PrimaryTags/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimaryTag primaryTag = db.PrimaryTags.Find(id);
            if (primaryTag == null)
            {
                return HttpNotFound();
            }
            return View(primaryTag);
        }

        // GET: PrimaryTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrimaryTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TagId,IP_Address,TagInformation1,TagInformation2")] PrimaryTag primaryTag)
        {
            if (ModelState.IsValid)
            {
                db.PrimaryTags.Add(primaryTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(primaryTag);
        }

        // GET: PrimaryTags/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimaryTag primaryTag = db.PrimaryTags.Find(id);
            if (primaryTag == null)
            {
                return HttpNotFound();
            }
            return View(primaryTag);
        }

        // POST: PrimaryTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TagId,IP_Address,TagInformation1,TagInformation2")] PrimaryTag primaryTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primaryTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(primaryTag);
        }

        // GET: PrimaryTags/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimaryTag primaryTag = db.PrimaryTags.Find(id);
            if (primaryTag == null)
            {
                return HttpNotFound();
            }
            return View(primaryTag);
        }

        // POST: PrimaryTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PrimaryTag primaryTag = db.PrimaryTags.Find(id);
            db.PrimaryTags.Remove(primaryTag);
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
