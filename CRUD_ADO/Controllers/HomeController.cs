using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_ADO.Models;

namespace CRUD_ADO.Controllers
{
    public class HomeController : Controller
    {
        DbServices db = new DbServices();
        public ActionResult Index()
        {
            return View(db.GetData());
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var row = db.GetData().Find(model => model.ID == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(EmpModel obj)
        {          
                db.Del(obj);
                return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var row = db.GetData().Find(model => model.ID == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Details()
        {
            db.GetData();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var row = db.GetData().Find(model => model.ID == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(EmpModel obj)
        {
            if (ModelState.IsValid == true)
            {
                db.Update(obj);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.DeleteMsg = "<script>alert('Something went wrong')</script>";
            }
            return View();


        }




        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EmpModel e)
        {
            if (ModelState.IsValid == true)
            {
                db.Add(e);
                if (db != null)
                {
                    ViewBag.AddMsg = "<script>alert('Something went wrong')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.AddMsg = "<script>alert('Data saved....')</script>";
                }
            }
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.AbMessage = "This a CRUD application build using asp.net and ado.net.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.ContMessage = "Contact Details";

            return View();
        }

        public ActionResult Age()
        {
            ViewBag.AgMessage = "Age Details";

            return View();
        }
    }
}