using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using VigneshProject.Models;

namespace VigneshProject.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
         // Session["User_Id"]=  User.Identity.GetUserId();
           
            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}