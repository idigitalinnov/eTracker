using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using A11_RBS.Domain;
using A11_RBS.Infrastructure.Services;
using A11_RBS.Infrastructure.Repository;

namespace A11_RBS.Controllers
{
    public class UserProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /UserProfile/
        public ActionResult Index()
        {
            return View(db.UserProfiles.ToList());
        }


        public ActionResult EmployeeDetails()
        {
            if (UserExtensions.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var AppId=UserExtensions.GetUserId();
            Repository<ApplicationUser> apprepo = new Repository<ApplicationUser>();
            ApplicationUser user = apprepo.Get(filter: m => m.Id == AppId, includeProperties: "Userprofile").FirstOrDefault<ApplicationUser>(); ;
            UserProfile userprofile = user.Userprofile;

            if (userprofile == null)
            {
                return RedirectToAction("Create");
            }

            return View(userprofile);
        }


        // GET: /UserProfile/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // GET: /UserProfile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FirstName,MiddleName,LastName,Email,Phone,BirthDate,JoiningDate,CurrentAddress,EmployeePosition,EmployeStatus,LeavesAvailable,CreatedTime")] UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                userprofile.Id = Guid.NewGuid();
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        // GET: /UserProfile/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FirstName,MiddleName,LastName,Email,Phone,BirthDate,JoiningDate,CurrentAddress,EmployeePosition,EmployeStatus,LeavesAvailable,CreatedTime")] UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        // GET: /UserProfile/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
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
