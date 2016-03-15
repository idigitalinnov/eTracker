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
using A11_RBS.CustomFilters;
using A11_RBS.Models;
using A11_RBS.Infrastructure.EnumsExtensions;
using AutoMapper;
using A11_RBS.Infrastructure.Repository;

namespace A11_RBS.Controllers
{
    public class LeaveController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Repository<ApplicationUser> user = new Repository<ApplicationUser>();
        // GET: /Leave/
        public ActionResult Index()
        {
          
                List<Leaves> use = db.Users.Find(UserExtensions.GetUserId()).LeavesRequests.OrderByDescending(m => m.IsApproved).ToList<Leaves>();
                return View(use);
           
        }

        // GET: /Leave/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leaves leaves = db.Leaves.Find(id);
            if (leaves == null)
            {
                return HttpNotFound();
            }
            return View(leaves);
        }

        // GET: /Leave/Create
        public ActionResult Create()
        {
            return View();
        }


        //
        // POST: /Leave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveType,Reason,NoOfDays,FromDate,ToDate,Comments")]Leaves leaves)
        {
            if (ModelState.IsValid)
            {
                leaves.Id = Guid.NewGuid();

                leaves.RequestedDateTime = DateTime.Now;
                leaves.IsApproved = LeaveApprovalStatus.Pending;
                ApplicationUser use = db.Users.Find(UserExtensions.GetUserId());
                use.LeavesRequests.Add(leaves);
                db.SaveChanges();



                return RedirectToAction("Index");
            }

            return View(leaves);
        }

        // GET: /Leave/Edit/5
       
        [AuthLog(Roles="Office Manager, Project Manager,Super Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leaves leaves = db.Leaves.Find(id);
            if (leaves == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: /Leave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LeaveType,Reason,NoOfDays,RequestedDateTime,FromDate,ToDate,IsApproved,RespondedDateTime,Comments")]Leaves leaves)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaves).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaves);
        }

        // GET: /Leave/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leaves leaves = db.Leaves.Find(id);
            if (leaves == null)
            {
                return HttpNotFound();
            }
            return View(leaves);
        }

        // POST: /Leave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Leaves leaves = db.Leaves.Find(id);
            db.Leaves.Remove(leaves);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



          [AuthLog(Roles="Super Admin, Office Manager, Project Manager")]  
        public ActionResult LeaveRequests()
        {
            string id = UserExtensions.GetUserId();
            List<ApplicationUser> LeavesForApproval = user.Get(filter: m => m.Id != id, includeProperties: "LeavesRequests").ToList<ApplicationUser>();


            List<Leaves> use =new List<Leaves>();
            foreach (var m in LeavesForApproval)
            {
                foreach (var l in m.LeavesRequests)
                {
                    if (l.IsApproved == LeaveApprovalStatus.Pending)
                    {
                        use.Add(l);
                    }
                }
            }
                return View(use);
            
        }


            [HttpPost]
          public ActionResult LeaveRequests(IList<Leaves> LeaveApprovals)
          {
              if (ModelState.IsValid)
              {
                  foreach (Leaves b in LeaveApprovals)
                  {
                      
                      db.Entry(b).State = EntityState.Modified;
                  }
                  db.SaveChanges();
                  return View("Index", "Home");
              }
              else
              {
                  return View();
              }
          }


        
             public ActionResult LeaveApproved(Guid Id)
            {
                if (Id != null )
                {
                    Leaves l = db.Leaves.Find(Id);
                    l.IsApproved = LeaveApprovalStatus.Approved;
                    db.SaveChanges();
                    return RedirectToAction("LeaveRequests");
                }
                return View();
          }

          public ActionResult LeaveNotApproved(Guid Id)
            {
                if (Id != null )
                {
                    Leaves l = db.Leaves.Find(Id);
                    l.IsApproved = LeaveApprovalStatus.NotApproved;
                    db.SaveChanges();
                    return RedirectToAction("LeaveRequests");
                }
                return View();
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
