using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM.Pagination.Authentication.Models;
using PagedList;
using PagedList.Mvc;

namespace CRM.Pagination.Authentication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CRMDbContext db = new CRMDbContext();

        // GET: Account
        public ActionResult Index(string order,int? page)
        {
            var accounts = from acc in db.Accounts select acc;
            accounts = Sort(accounts, order);
            int pageNumber = page ?? 1;
            return View(accounts.ToPagedList(pageNumber,2));
        }
        private IQueryable<Account> Sort(IQueryable<Account> accounts,string order)
        {
            switch (order)
            {
                case "Name":
                    accounts = accounts.OrderBy(a => a.Name);
                    break;
                case "Interest":
                    accounts = accounts.OrderBy(a => a.Interest);
                    break;
                case "AddedDate":
                    accounts = accounts.OrderBy(a => a.AddedDate);
                    break;
                default:
                    accounts = accounts.OrderBy(a => a.Id);
                    break;
            }
            return accounts;
        }
        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel avm)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(new Account()
                {
                    Name = avm.Name,
                    Interest=avm.Interest,
                    MinimumBalance=avm.MinimumBalance,
                    AddedDate=DateTime.Now,
                    Status=avm.Status
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avm);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountViewModel avm)
        {
            if (ModelState.IsValid)
            {
                Account account = db.Accounts.Find(avm.Id);
                account.Id = avm.Id;
                account.Name = avm.Name;
                account.Interest = avm.Interest;
                account.MinimumBalance = avm.MinimumBalance;
                account.ModifiedDate = DateTime.Now;
                account.Status = avm.Status;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(avm);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
