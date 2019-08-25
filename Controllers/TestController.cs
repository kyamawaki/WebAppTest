using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Controllers
{
    public class SampModel
    {
    }

    public class Sample
    {
        [Range(1,10, ErrorMessage ="NO")]
        public int Value1 { get; set; }
        public List<string> List { get; set; }

        public Sample()
        {
            this.List = new List<string>(new string [] { "1", "5", "10" });
        }
    };

    public class TestController : Controller
    {
        private Sample sample = new Sample();

        // GET: Test
        public ActionResult Index()
        {
            ViewBag.List = new SelectList(this.sample.List);
            return View(sample);
        }

        // GET: Test/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(sample);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
