using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student.BusinessLogic;
using Layer.DataAccess;

namespace FINAL___Lactao_n_Magpatoc.Controllers
{
    public class StudentController : Controller
    {
        private StudentBLL studbll = new StudentBLL();
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult LoginFail()
        {
            return View();
        }
        public IActionResult View()
        {
            List<StudentBLL> view = studbll.GetAll();

            return View(view);
        }

        [HttpPost]

        public IActionResult Login(FormCollection data)
        {
            Account acc = new Account();
            List<Account> accounts = acc.GetAll();

            bool valid = false;

            foreach (Account item in accounts)
            {
                if (data["Username"].ToString().Equals(item.Username) && data["Password"].ToString().Equals(item.Password))
                    valid = true;
            }

            if (valid)
                return RedirectToAction("List", "Student");
            else
                return RedirectToAction("LoginFail", "Student");

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
