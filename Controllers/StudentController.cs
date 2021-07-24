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
        public ActionResult Home()
        {
            return View();
        }
    }
}
