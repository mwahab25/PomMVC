using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PomMVC.Models;

namespace PomMVC.Controllers
{
    [Authorize]
    public class VersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vers
        public ActionResult Index()
        {
            return View(db.Versions.ToList());
        }

        // GET: Vers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ver vers = db.Versions.Find(id);
            if (vers == null)
            {
                return HttpNotFound();
            }
            return View(vers);
        }

        // GET: Vers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VerID,VerNo")] Ver vers)
        {
            if (ModelState.IsValid)
            {
                db.Versions.Add(vers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vers);
        }

        // GET: Vers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ver vers = db.Versions.Find(id);
            if (vers == null)
            {
                return HttpNotFound();
            }
            return View(vers);
        }

        // POST: Vers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VerID,VerNo")] Ver vers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vers);
        }

        // GET: Vers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ver vers = db.Versions.Find(id);
            if (vers == null)
            {
                return HttpNotFound();
            }
            return View(vers);
        }

        // POST: Vers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ver vers = db.Versions.Find(id);
            db.Versions.Remove(vers);
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
