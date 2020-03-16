using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VigneshProject.Models;

namespace VigneshProject.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User

        public ActionResult Action()
        {
            
            return View(db.PropertyTypes.ToList());
        }
       
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            return View(db.Properties.Where(a=>a.UserID== userid).ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userid = User.Identity.GetUserId();
            var details= db.Properties.Where(a => a.UserID == userid).ToList();
            Property property = details.Where(a => a.PropID == id).FirstOrDefault();

            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            //ApplicationDbContext db = new ApplicationDbContext();

            ViewBag.Type = new SelectList(db.PropertyTypes, "PropType", "PropType");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropID,PropName,Location,PropOwnerName,PropDescription,PropType,PhoneNo")] Property property,HttpPostedFileBase imgfile)
        {
            property.UserID = User.Identity.GetUserId();
            string path = uploadfile(imgfile);
            property.PropImage = path;
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(property);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userid = User.Identity.GetUserId();
            var details = db.Properties.Where(a => a.UserID == userid).ToList();
            Property property = details.Where(a => a.PropID == id).FirstOrDefault();

            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropID,PropName,Location,PropOwnerName,PropDescription,PropType,PhoneNo,PropImage")] Property property,HttpPostedFileBase imgfile)
        {
            property.UserID = User.Identity.GetUserId();
            property.PropImage = uploadfile(imgfile);
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userid = User.Identity.GetUserId();
            var details = db.Properties.Where(a => a.UserID == userid).ToList();
            Property property = details.Where(a => a.PropID == id).FirstOrDefault();
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userid = User.Identity.GetUserId();
            var details = db.Properties.Where(a => a.UserID == userid).ToList();
            Property property = details.Where(a => a.PropID == id).FirstOrDefault();
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

        public ActionResult ViewProperty(string id="Rental")
        {
            List<Property> list = db.Properties.Where(x => x.PropType == id).ToList();
            return View(list);
        }
    }
}
