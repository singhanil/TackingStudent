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
    public class StudentDetailsController : Controller
    {
        private StudentTrackingEntities db = new StudentTrackingEntities();

        // GET: StudentDetails
        public ActionResult Index()
        {
            var studentDetails = db.StudentDetails.Include(s => s.Class).Include(s => s.PrimaryTag).Include(s => s.SchoolBranch).Include(s => s.SecondaryTag).Include(s => s.Section);
            return View(studentDetails.ToList());
        }

        // GET: StudentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // GET: StudentDetails/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId");
            ViewBag.SchoolBranchId = new SelectList(db.SchoolBranches, "Id", "Name");
            ViewBag.SecondaryTagId = new SelectList(db.SecondaryTags, "Id", "TagId");
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name");
            return View();
        }

        // POST: StudentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,ParentMobileNo,PrimaryTagId,SecondaryTagId,EmailId,StudentName,SchoolBranchId,ClassId,SectionId")] StudentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.StudentDetails.Add(studentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", studentDetail.ClassId);
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId", studentDetail.PrimaryTagId);
            ViewBag.SchoolBranchId = new SelectList(db.SchoolBranches, "Id", "Name", studentDetail.SchoolBranchId);
            ViewBag.SecondaryTagId = new SelectList(db.SecondaryTags, "Id", "TagId", studentDetail.SecondaryTagId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", studentDetail.SectionId);
            return View(studentDetail);
        }

        // GET: StudentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", studentDetail.ClassId);
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId", studentDetail.PrimaryTagId);
            ViewBag.SchoolBranchId = new SelectList(db.SchoolBranches, "Id", "Name", studentDetail.SchoolBranchId);
            ViewBag.SecondaryTagId = new SelectList(db.SecondaryTags, "Id", "TagId", studentDetail.SecondaryTagId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", studentDetail.SectionId);
            return View(studentDetail);
        }

        // POST: StudentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,ParentMobileNo,PrimaryTagId,SecondaryTagId,EmailId,StudentName,SchoolBranchId,ClassId,SectionId")] StudentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", studentDetail.ClassId);
            ViewBag.PrimaryTagId = new SelectList(db.PrimaryTags, "Id", "TagId", studentDetail.PrimaryTagId);
            ViewBag.SchoolBranchId = new SelectList(db.SchoolBranches, "Id", "Name", studentDetail.SchoolBranchId);
            ViewBag.SecondaryTagId = new SelectList(db.SecondaryTags, "Id", "TagId", studentDetail.SecondaryTagId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", studentDetail.SectionId);
            return View(studentDetail);
        }

        // GET: StudentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // POST: StudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            db.StudentDetails.Remove(studentDetail);
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
