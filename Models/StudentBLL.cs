using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Layer.DataAccess;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Student.BusinessLogic
{
    public class StudentBLL 
    {

        public int StudentID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name should not be less than 2 characters")]
        [StringLength(50, ErrorMessage = "Last name must be not more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must be letters only")]
        public string Lastname { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name should not be less than 2 characters")]
        [StringLength(50, ErrorMessage = "First name must be not more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must be letters only")]
        public string Firstname { get; set; }

        private MssqlDAL dal = new MssqlDAL();

        public void Add()
        {
            dal.Open();
            dal.SetSql("INSERT INTO Students " + "(Lastname, Firstname)" +
                "VALUES (@a, @b)");
            dal.AddParameter("@a", Lastname);
            dal.AddParameter("@b", Firstname);
            dal.Execute();
            dal.Close();
        }
        public List<StudentBLL> GetAll()
        {
            List<StudentBLL> list = new List<StudentBLL>();
            dal.Open();
            dal.SetSql("SELECT * FROM Students");
            SqlDataReader dr = dal.GetReader();
            while(dr.Read() ==  true)
            {
                StudentBLL s = new StudentBLL(); //small letter s for student
                s.StudentID = (int)dr["StudentID"];
                s.Lastname= dr["Lastname"].ToString();
                s.Firstname = dr["Firstname"].ToString();
               
                list.Add(s); ;
            }
            dr.Close();
            dal.Close();
            return list;
        }

        public StudentBLL Get(int id)
        {
            StudentBLL s = new StudentBLL();

            dal.Open();
            dal.SetSql("SELECT * FROM Students WHERE StudentID = @id");
            dal.AddParameter("@id", id);
            SqlDataReader dr = dal.GetReader();
            if (dr.Read() == true)
            {
                s.StudentID = (int)dr[0];
                s.Lastname = dr[1].ToString();
                s.Firstname = dr[2].ToString();
            }
            dr.Close();
            dal.Close();
            return s;
        }
        public void Edit()
        {
            dal.Open();
            dal.SetSql("UPDATE Students " +
                "SET    Lastname = @a, " +
                "       Firstname = @b " +
                "WHERE StudentID = @id");
            dal.AddParameter("@a", Lastname);
            dal.AddParameter("@b", Firstname);
            dal.AddParameter("@id", StudentID);
            dal.Execute();
            dal.Close();
        }
        public void Delete()
        {
            dal.Open();
            dal.SetSql("DELETE Students WHERE StudentID = @id");
            dal.AddParameter("@id", StudentID);
            dal.Execute();
            dal.Close();
        }

        public List<StudentBLL> Search(string key)
        {
            List<StudentBLL> list = new List<StudentBLL>();

            dal.Open();
            dal.SetSql("SELECT * FROM Students " +
                "WHERE FirstName LIKE @key OR LastName LIKE @key ");
            dal.AddParameter("@key", "%" + key + "%");

            SqlDataReader dr = dal.GetReader();

            while (dr.Read() == true)
            {
                StudentBLL s = new StudentBLL();
                s.StudentID = (int)dr["StudentID"];
                s.Firstname = dr["FirstName"].ToString();
                s.Lastname = dr["LastName"].ToString();

                list.Add(s);
            }

            dr.Close();
            dal.Close();

            return list;
        }


    }

}