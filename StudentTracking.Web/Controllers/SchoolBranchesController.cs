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
    public class SchoolBranchesController : Controller
    {
        private StudentTrackingEntities db = new StudentTrackingEntities();

        // GET: SchoolBranches
        public ActionResult Index()
        {
            var schoolBranches = db.SchoolBranches.Include(s => s.School);
            return View(schoolBranches.ToList());
        }

        // GET: SchoolBranches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolBranch schoolBranch = db.SchoolBranches.Find(id);
            if (schoolBranch == null)
            {
                return HttpNotFound();
            }
            return View(schoolBranch);
        }

        // GET: SchoolBranches/Create
        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View();
        }

        // POST: SchoolBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,SchoolId,Contact_Detail,Branch_Code")] SchoolBranch schoolBranch)
        {
            if (ModelState.IsValid)
            {
                db.SchoolBranches.Add(schoolBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolBranch.SchoolId);
            return View(schoolBranch);
        }

        // GET: SchoolBranches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolBranch schoolBranch = db.SchoolBranches.Find(id);
            if (schoolBranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolBranch.SchoolId);
            return View(schoolBranch);
        }

        // POST: SchoolBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,SchoolId,Contact_Detail,Branch_Code")] SchoolBranch schoolBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolBranch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolBranch.SchoolId);
            return View(schoolBranch);
        }

        // GET: SchoolBranches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolBranch schoolBranch = db.SchoolBranches.Find(id);
            if (schoolBranch == null)
            {
                return HttpNotFound();
            }
            return View(schoolBranch);
        }

        // POST: SchoolBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolBranch schoolBranch = db.SchoolBranches.Find(id);
            db.SchoolBranches.Remove(schoolBranch);
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
