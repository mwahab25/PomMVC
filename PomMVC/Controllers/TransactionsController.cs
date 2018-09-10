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
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index(string sortOrder, string searchString)
        {
            var transactions = db.Transactions.Include(t => t.Page).Include(t => t.Project).Include(t => t.User).Include(t => t.Vers);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            transactions = from s in db.Transactions
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(s => s.User.Email.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    transactions = transactions.OrderByDescending(s => s.User);
                    break;
                case "Date":
                    transactions = transactions.OrderBy(s => s.TransDate);
                    break;
                case "date_desc":
                    transactions = transactions.OrderByDescending(s => s.TransDate);
                    break;
                default:
                    transactions = transactions.OrderByDescending(s => s.TransDate);
                    break;
            }
           
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.PageID = new SelectList(db.Pages, "PageID", "PageName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.VersID = new SelectList(db.Versions, "VersID", "VersNo");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransID,UserId,ProjectID,VersID,PageID,TransType,TransDate,TransFunEle,TransText")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PageID = new SelectList(db.Pages, "PageID", "PageName", transaction.PageID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", transaction.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", transaction.UserId);
            ViewBag.VersID = new SelectList(db.Versions, "VersID", "VersNo", transaction.VersID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageID = new SelectList(db.Pages, "PageID", "PageName", transaction.PageID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", transaction.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", transaction.UserId);
            ViewBag.VersID = new SelectList(db.Versions, "VersID", "VersNo", transaction.VersID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransID,UserId,ProjectID,VersID,PageID,TransType,TransDate,TransFunEle,TransText")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PageID = new SelectList(db.Pages, "PageID", "PageName", transaction.PageID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", transaction.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", transaction.UserId);
            ViewBag.VersID = new SelectList(db.Versions, "VersID", "VersNo", transaction.VersID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
