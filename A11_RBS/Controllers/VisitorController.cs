using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using A11_RBS.Domain;
using A11_RBS.CustomFilters;

namespace A11_RBS.Controllers
{
    public class VisitorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Visitor/
        public ActionResult Index()
        {
            return View(db.InterviewRegistration.ToList());
        }

        // GET: /Visitor/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitors visitors = db.InterviewRegistration.Find(id);
            if (visitors == null)
            {
                return HttpNotFound();
            }
            return View(visitors);
        }

        // GET: /Visitor/Create
        public ActionResult Create()
        {
            List<string> experience = new List<string>(){
                "Fresher",
                "1-3 Years",
                "3-7 Years",
                "7+ Years"
            };

            ViewBag.Experience = new SelectList(experience);
            List<string> know = new List<string>(){
                "Facebook",
                "Direct Website",
                "Through Employee",
                "Friends"
            };

            ViewBag.KnowaboutUse = new SelectList(know);
            List<string> ExpectedJoin = new List<string>(){
                "Immediate",
                "With in 15 days",
                "With in a month",
                "With in 3 months",
                "More than 3 months"
            };

            ViewBag.ExpectedJoining = new SelectList(ExpectedJoin);
            return View();
        }

        // POST: /Visitor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,FatherName,Email, Phone,Qualification,Stream,Skills,Experience, CurrentCTC, ExpectedJoining, KnowaboutUse")] Visitors visitors)
        {
            if (ModelState.IsValid)
            {
                visitors.Id = Guid.NewGuid();
                db.InterviewRegistration.Add(visitors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<string> experience = new List<string>(){
                "Fresher",
                "1-3 Years",
                "3-7 Years",
                "7+ Years"
            };

            ViewBag.Experience = new SelectList(experience);
            List<string> know = new List<string>(){
                "Facebook",
                "Direct Website",
                "Through Employee",
                "Friends"
            };

            ViewBag.KnowaboutUse = new SelectList(know);

            List<string> ExpectedJoin = new List<string>(){
                "Immediate",
                "With in 15 days",
                "With in a month",
                "With in 3 months",
                "More than 3 months"
            };

            ViewBag.ExpectedJoining = new SelectList(ExpectedJoin);
            
            return View(visitors);
        }

        // GET: /Visitor/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitors visitors = db.InterviewRegistration.Find(id);
            if (visitors == null)
            {
                return HttpNotFound();
            }
            return View(visitors);
        }

        // POST: /Visitor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,FatherName,Email, Phone,Qualification,Stream,Skills,Experience,CurrentCTC,ExpectedJoining,KnowaboutUse")] Visitors visitors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visitors);
        }

        // GET: /Visitor/Delete/5
        [AuthLog(Roles="Super Admin, Office Manager, Project Manager")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitors visitors = db.InterviewRegistration.Find(id);
            if (visitors == null)
            {
                return HttpNotFound();
            }
            return View(visitors);
        }

        // POST: /Visitor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Visitors visitors = db.InterviewRegistration.Find(id);
            db.InterviewRegistration.Remove(visitors);
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
