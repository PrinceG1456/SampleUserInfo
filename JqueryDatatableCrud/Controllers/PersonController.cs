using JqueryDatatableCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqueryDatatableCrud.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DbModel1 _context = new DbModel1())
            {
                List<Person> personList = _context.People.ToList();
                return Json(new { data = personList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EditOrAdd(int id = 0)
        {
            if (id == 0)
                return View("EditOrAdd", new Person());
            else
            {
                using (DbModel1 _context = new DbModel1())
                {
                   return View("EditOrAdd", _context.People.Where(x => x.id == id).FirstOrDefault<Person>());
                }
            }
        }


        [HttpPost]
        public ActionResult EditOrAdd(Person p)
        {

            using (DbModel1 _context = new DbModel1())
            {
                if (p.id == 0)
                {
                    _context.People.Add(p);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Saved Succesfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Updated Succesfully" }, JsonRequestBehavior.AllowGet);
                }
            }

            

        }
    }
}