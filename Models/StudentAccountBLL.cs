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
                yield return new ValidationResult("Incorrect Username or Password.", property);
            }
        }

    }
}
