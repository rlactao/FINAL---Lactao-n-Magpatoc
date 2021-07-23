using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intepro.BusinessLogic;

namespace FINAL___Lactao_n_Magpatoc.Controllers
{
    public class StudentController : Controller
    {
        private StudentBLL studbll = new StudentBLL();
        public IActionResult View()
        {
            List<StudentBLL> view = studbll.GetAll();

            return View(view);
        }
        public IActionResult Add()
        {
            return View(new StudentBLL());
        }
        [HttpPost]
        public IActionResult Add(StudentBLL obj)
        {
            if (ModelState.IsValid)
            {
                obj.Add();
                return RedirectToAction("View");
            }
            else
                return View(obj);
        }
        public IActionResult Edit(int aID)
        {
            StudentBLL obj = studbll.Get(aID);
            if (obj.StudentID == 0)
                return RedirectToAction("View");

            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(StudentBLL obj)
        {
            if (ModelState.IsValid)
            {
                obj.Edit();
                return RedirectToAction("View");
            }
            else
                return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(StudentBLL obj)
        {
            obj.Delete();
            return RedirectToAction("View");

        }

    }
}
