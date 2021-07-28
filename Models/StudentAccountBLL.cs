using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Layer.DataAccess;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using FINAL___Lactao_n_Magpatoc.Models;

namespace Student.Account.BusinessLogic
{
    public class StudentAccountBLL : IValidatableObject
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        MssqlDAL dal = new MssqlDAL();

        //public List<StudentAccountBLL> GetAll()
        //{
        //    List<StudentAccountBLL> list = new List<StudentAccountBLL>();
        //    dal.Open();
        //    dal.SetSql("SELECT * FROM UserAccounts"); 
        //    SqlDataReader dr = dal.GetReader();

        //    while (dr.Read() == true)
        //    {
        //        StudentAccountBLL acct = new StudentAccountBLL();
        //        acct.Username = dr["Username"].ToString();
        //        acct.Password = dr["Password"].ToString();
                
        //        list.Add(acct);
        //    }

        //    dr.Close();
        //    dal.Close();

        //    return list;
        //}

        public bool TryValidate()
        {
            dal.Open();
            dal.SetSql("SELECT * FROM UserAccounts WHERE Username = @a COLLATE SQL_Latin1_General_CP1_CS_AS " +
                        "AND Password = @b COLLATE SQL_Latin1_General_CP1_CS_AS");
            dal.AddParameter("@a", Username);
            dal.AddParameter("@b", Password);
            DataTable dta = dal.GetData();

            if (dta.Rows.Count == 1)
            {
                dal.Close();
                return true;
            }
            else
            {
                dal.Close();
                return false;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var property = new[] { "Username" };

            if (this.TryValidate() == false)
            {
                yield return new ValidationResult("Wrong Username/Password", property);
            }
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
