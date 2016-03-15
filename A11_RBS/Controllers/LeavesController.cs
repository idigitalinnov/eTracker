using A11_RBS.Domain;
using A11_RBS.Infrastructure.EnumsExtensions;
using A11_RBS.Infrastructure.Repository;
using A11_RBS.Infrastructure.Services;
using A11_RBS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace A11_RBS.Controllers
{
    public class LeavesController : Controller
    {
        //
        // GET: /Leaves/

        

        
        Repository<ApplicationUser> apprepo = new Repository<ApplicationUser>();

        public ActionResult Index()
        {
            string userId = UserExtensions.GetUserId();
            IList<Leaves> PreviousLeaves = apprepo.GetById(UserExtensions.GetUserId()).LeavesRequests.ToList<Leaves>();

            return View(PreviousLeaves);
        }

        public ActionResult ApplyForLeave()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyForLeave([Bind(Exclude = ("ApprovedBy,RequestedDateTime,RespondedDateTime,IsApproved,Comments"))]Leaves model)
        {

            if (ModelState.IsValid)
            {

                ApplicationUser use = apprepo.GetById(UserExtensions.GetUserId());
                use.LeavesRequests.Add(model);
                apprepo.Edit(use);
               


                return RedirectToAction("Index", "Leaves");
            }

            return View(model);
        }




    }
}