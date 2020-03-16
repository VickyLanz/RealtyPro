using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using VigneshProject.Models;

namespace VigneshProject.Controllers
{
    [Authorize(Roles ="User")]
    public class PropertiesController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
         

        // GET: Properties
      
        public ActionResult Index()
        {
            return View(db.Properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            ViewBag.Type = new SelectList(db.PropertyTypes, "PropType", "PropType");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropID,PropName,Location,PropOwnerName,PropDescription,PropType,PhoneNo")] Property property,HttpPostedFileBase imgfile)
        {
           property.UserID= User.Identity.GetUserId();
            string path = uploadfile(imgfile);
            //string userId = Common.ExtensionMethod.getUserId();
            if (ModelState.IsValid)
            {
                ////////////S property.UserID = userId;
                property.PropImage = path;
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(property);
            }

           
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropID,PropName,Location,PropOwnerName,PropDescription,PropType,PhoneNo,PropImage,UserID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
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
        public string uploadfile(HttpPostedFileBase imgfile)
        {
            Random r = new Random();
            int random = r.Next();
            string path = Path.Combine(Server.MapPath("~/Image/"), random + Path.GetFileName(imgfile.FileName));
            imgfile.SaveAs(path);
            path = "~/Image/" + random + Path.GetFileName(imgfile.FileName);
            return path;
        }


    }
}
