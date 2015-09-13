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
    public class StaffDetailsController : Controller
    {
        private StudentTrackingEntities db = new StudentTrackingEntities();

        // GET: StaffDetails
        public ActionResult Index()
        {
            var staffDetails = db.StaffDetails.Include(s => s.Class).Include(s => s.PrimaryTag).Include(s => s.Section);
            return View(staffDetails.ToList());
        }

        // GET: StaffDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            if (staffDetail == null)
            {
                return HttpNotFound();
            }
            return View(staffDetail);
        }

        // GET: StaffDetails/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId");
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name");
            return View();
        }

        // POST: StaffDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StaffId,StaffName,Department,PrimaryTagId,ReportingEmailId,StaffMobileNo,ClassId,SectionId")] StaffDetail staffDetail)
        {
            if (ModelState.IsValid)
            {
                db.StaffDetails.Add(staffDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", staffDetail.ClassId);
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId", staffDetail.PrimaryTagId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", staffDetail.SectionId);
            return View(staffDetail);
        }

        // GET: StaffDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            if (staffDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", staffDetail.ClassId);
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId", staffDetail.PrimaryTagId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", staffDetail.SectionId);
            return View(staffDetail);
        }

        // POST: StaffDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StaffId,StaffName,Department,PrimaryTagId,ReportingEmailId,StaffMobileNo,ClassId,SectionId")] StaffDetail staffDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", staffDetail.ClassId);
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId", staffDetail.PrimaryTagId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", staffDetail.SectionId);
            return View(staffDetail);
        }

        // GET: StaffDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            if (staffDetail == null)
            {
                return HttpNotFound();
            }
            return View(staffDetail);
        }

        // POST: StaffDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffDetail staffDetail = db.StaffDetails.Find(id);
            db.StaffDetails.Remove(staffDetail);
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
