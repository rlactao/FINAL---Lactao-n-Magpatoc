using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Layer.DataAccess;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Student.Account.BusinessLogic
{
    public class StudentAccountBLL
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        MssqlDAL dal = new MssqlDAL();

        public List<StudentAccountBLL> GetAll()
        {
            List<StudentAccountBLL> list = new List<StudentAccountBLL>();
            dal.Open();
            dal.SetSql("SELECT * FROM UserAccounts"); 
            SqlDataReader dr = dal.GetReader();

            while (dr.Read() == true)
            {
                StudentAccountBLL acct = new StudentAccountBLL();
                acct.Username = dr["Username"].ToString();
                acct.Password = dr["Password"].ToString();
                
                list.Add(acct);
            }

            dr.Close();
            dal.Close();

            return list;
        }

        //[HttpPost]

        //public IActionResult Login(FormCollection data)
        //{
        //    StudentAccountBLL acct = new StudentAccountBLL();
        //    List<StudentAccountBLL> accounts = acct.GetAll();

        //    bool valid = false;

        //    foreach (StudentAccountBLL item in accounts)
        //    {
        //        if (data["Username"].ToString().Equals(item.Username) && data["Password"].ToString().Equals(item.Password))
        //            valid = true;
        //    }

        //    if (valid)
        //        return RedirectToAction("List", "Student");
        //    else
        //        return RedirectToAction("LoginFail", "Student");

        //}
    }
}
