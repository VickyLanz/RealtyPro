using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VigneshProject.Models;

namespace VigneshProject.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        ApplicationDbContext context;
        public RolesController()
        {

            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }
        public ActionResult Create()
        {
            var Roles = new IdentityRole();
            return View(Roles);
        }
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index", "Role");
        }
        public ActionResult AssignRoleToUser()
        {
            return View();
        }
    }
}