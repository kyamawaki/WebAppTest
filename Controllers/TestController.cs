using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class Sample
    {
        public List<TestTable> TestTables { get; set; }
    };

    public class TestController : Controller
    {
        private TestEntities1 db = new TestEntities1();

        private Sample sample = new Sample();

        // GET: Test
        public ActionResult Index()
        {
            this.sample.TestTables = db.TestTable.ToList();
            return View(sample);
        }

        // GET: Test/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTable testTable = db.TestTable.Find(id);
            if (testTable == null)
            {
                return HttpNotFound();
            }
            return View(testTable);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Value")] TestTable testTable)
        {
            if (ModelState.IsValid)
            {
                db.TestTable.Add(testTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testTable);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTable testTable = db.TestTable.Find(id);
            if (testTable == null)
            {
                return HttpNotFound();
            }
            return View(testTable);
        }

        // POST: Test/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Value")] TestTable testTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testTable);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTable testTable = db.TestTable.Find(id);
            if (testTable == null)
            {
                return HttpNotFound();
            }
            return View(testTable);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TestTable testTable = db.TestTable.Find(id);
            db.TestTable.Remove(testTable);
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

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Sample model)
        {
            for(int i=0; i<model.TestTables.Count; i++)
            {
                this.sample.TestTables[i].Value = model.TestTables[i].Value;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
