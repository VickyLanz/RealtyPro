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
        ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index()
        {
            //db.Roles.
            return View();
        }
    }
}