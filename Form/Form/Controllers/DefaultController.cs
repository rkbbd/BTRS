using Form.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Form.Controllers
{
    public class DefaultController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Default
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
     
        public ActionResult Create()
        {
            //List<SelectListItem> items = new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "----Select One----", Value= "" },
            //     new SelectListItem { Text = "CSE", Value= "1" },
            //      new SelectListItem { Text = "EEE", Value= "2" },
            //     new SelectListItem { Text = "Management", Value= "3" }
            //};
            ViewBag.Departmet = db.Departments.ToList();
            
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student students)
        {
            //List<SelectListItem> items = new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "----Select One----", Value= "" },
            //     new SelectListItem { Text = "CSE", Value= "1" },
            //      new SelectListItem { Text = "EEE", Value= "2" },
            //     new SelectListItem { Text = "Management", Value= "3" }
            //};
            ViewBag.Departmet = db.Departments.ToList(); 

            db.Students.Add(students);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult GetStudentByDeptId(int deptID)
        {
            var studentlist = db.Students.Where(x => x.DeptId == deptID);
            return Json(studentlist);
        }
    }
}