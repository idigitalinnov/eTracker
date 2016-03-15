using System;
using System.Linq;
using System.Web.Mvc;
using A11_RBS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using A11_RBS.CustomFilters;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using A11_RBS.Domain;

namespace A11_RBS.Controllers
{
    [AuthLog(Roles = "Super Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext(); 
        }
        

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        
        /// <summar
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /// <summary>
        /// Delete Role for Users
        /// </summary>
        /// <returns></returns>

        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Set Role for Users
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRoleToUser()
        {
            var list = context.Roles.OrderBy(role => role.Name).ToList().Select(role => new SelectListItem { Value = role.Name.ToString(), Text = role.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }


        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>

            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var Userlist = context.Users.OrderBy(usr => usr.UserName).ToList().Select(role => new SelectListItem { Value = role.UserName.ToString(), Text = role.UserName }).ToList();
            ViewBag.Userlist = Userlist;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserAddToRole(string Username, string RoleName)
        {
            ApplicationUser user = context.Users.Where(usr => usr.UserName.Equals(Username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            // Display All Roles in DropDown

            var list = context.Roles.OrderBy(role => role.Name).ToList().Select(role => new SelectListItem { Value = role.Name.ToString(), Text = role.Name }).ToList();
            ViewBag.Roles = list;

            var Userlist = context.Users.OrderBy(usr => usr.UserName).ToList().Select(role => new SelectListItem { Value = role.UserName.ToString(), Text = role.UserName }).ToList();
            ViewBag.Userlist = Userlist;
            if (user != null)
            {
                using (var us = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                {
                    IdentityResult result =await us.AddToRoleAsync(user.Id, RoleName);
                     
                }

                
                return View("ManageUserRoles");
            }
            else
            {
                ViewBag.ErrorMessage = "Sorry user is not available";
                 return View("ManageUserRoles");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                using (var us = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                {  

                 Task<IList<string>> RolesforUser = us.GetRolesAsync(user.Id);
                 ViewBag.RolesForThisUser = await RolesforUser;
                }
                // prepopulat roles for the view dropdown
            }

            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var Userlist = context.Users.OrderBy(usr => usr.UserName).ToList().Select(role => new SelectListItem { Value = role.UserName.ToString(), Text = role.UserName }).ToList();
            ViewBag.Userlist = Userlist;


            return View("ManageUserRoles");
        }

        

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            using (var us = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                us.AddToRoleAsync(user.Id, RoleName);
            }
            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var Userlist = context.Users.OrderBy(usr => usr.UserName).ToList().Select(role => new SelectListItem { Value = role.UserName.ToString(), Text = role.UserName }).ToList();
            ViewBag.Userlist = Userlist;

            return View("ManageUserRoles");
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleForUser(string UserName, string RoleName)
        {
            using (var us = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                bool isinrole =await us.IsInRoleAsync(user.Id, RoleName);
                if (isinrole)
                {
                    await us.RemoveFromRoleAsync(user.Id, RoleName);
                    ViewBag.ResultMessage = "Role removed from this user successfully !";
                }
                else
                {
                    ViewBag.ResultMessage = "This user doesn't belong to selected role.";
                }
                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;

                var Userlist = context.Users.OrderBy(usr => usr.UserName).ToList().Select(role => new SelectListItem { Value = role.UserName.ToString(), Text = role.UserName }).ToList();
                ViewBag.Userlist = Userlist;
            }
            return View("ManageUserRoles");
        }

    }
}