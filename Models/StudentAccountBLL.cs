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
            dal.SetSql("SELECT * FROM Accounts"); //pakimatch sa ginawa mo rae yung table name haha
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
    }
}
