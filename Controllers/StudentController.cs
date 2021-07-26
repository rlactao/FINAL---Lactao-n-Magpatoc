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
            return View();
        }
        public IActionResult View()
        {
            List<StudentBLL> view = studbll.GetAll();

            return View(view);
        }
        [HttpPost]

        public IActionResult Home(StudentAccountBLL obj) 
        {
            StudentAccountBLL studAcc = new StudentAccountBLL();
            List<StudentAccountBLL> account = studAcc.GetAll();

            bool valid = false;

            foreach (StudentAccountBLL item in account)
            {
                if (obj["Username"].ToString().Equals(item.Username) && obj["Password"].ToString().Equals(item.Password))
                    valid = true;
            }

            if (valid)
                return RedirectToAction("List", "Student");
           // else
               // return RedirectToAction("LoginFail", "Student");
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

        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction("View", "Student");

            StudentBLL studbll = new StudentBLL();
            studbll = studbll.Get(id);

            if (studbll.StudentID == 0)
                return RedirectToAction("View", "Student");
            else
                return View(studbll);
        }

        [HttpPost]

        public IActionResult Edit(StudentBLL model)
        {
            model.Edit();

            return RedirectToAction("List", "Student");
        }
     
        [HttpPost]
        public IActionResult Delete(StudentBLL model)
        {
            model.Delete();

            return RedirectToAction("List", "Student");
        }
        //public IActionResult Search()
        // {
        //    List<StudentBLL> view = studbll.GetAll();

       //     return View(view);
       // }
        //[HttpGet]
        //public IActionResult Search(StudentBLL obj)
       // {
       //    StudentBLL obj =  studbll.Get(obj)
       // }
        
    }
}
