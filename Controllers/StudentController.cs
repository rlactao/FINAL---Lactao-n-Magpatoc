using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student.BusinessLogic;
using Student.Account.BusinessLogic;
using Layer.DataAccess;


namespace FINAL___Lactao_n_Magpatoc.Controllers
{
    public class StudentController : Controller
    {
        private StudentBLL studbll = new StudentBLL();
        private StudentAccountBLL studAcctBLL = new StudentAccountBLL();
        public IActionResult Home()
        {
            return View(new StudentAccountBLL());
        }
        public IActionResult List()
        {
            List<StudentBLL> list = studbll.GetAll();

            return View(list);
        }

        [HttpPost]
        public IActionResult Home(StudentAccountBLL obj) 
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("List", "Student");
            }
            else
            {
                return View(obj);
            }


            //if (ModelState.IsValid)
            //{
            //    StudentAccountBLL studAcc = new StudentAccountBLL();
            //    List<StudentAccountBLL> account = studAcc.GetAll();

            //    bool valid = false;

            //    foreach (StudentAccountBLL item in account)
            //    {
            //        if (obj.Username.Equals(item.Username) && obj.Password.Equals(item.Password))
            //        {
            //            valid = true;
            //            break;
            //        }
            //        else
            //            valid = false;
            //    }

            //    if (valid)
            //        return RedirectToAction("List", "Student");
            //    else
            //    {
            //        return View(obj);
            //        // show error message that it is wrong username/password??
            //    }
            //}
            //else
            //{
            //    return View(obj);
            //}

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
                return RedirectToAction("List");
            }
            else
                return View(obj);
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction("List", "Student");

            StudentBLL studbll = new StudentBLL();
            studbll = studbll.Get(id);

            if (studbll.StudentID == 0)
                return RedirectToAction("List", "Student");
            else
                return View(studbll);
        }

        [HttpPost]
        public IActionResult Edit(StudentBLL model)
        {
            if (ModelState.IsValid)
            {
                model.Edit();
                return RedirectToAction("List", "Student");
            }
            else
            {
                return View(model);
            }
        }
     
        [HttpPost]
        public IActionResult Delete(StudentBLL model)
        {
            model.Delete();

            return RedirectToAction("List", "Student");
        }

        [HttpGet]
        public IActionResult List(string key)
        {
            List<StudentBLL> list = studbll.Search(key);

            return View(list);
        }

    }
}
