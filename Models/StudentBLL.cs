﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Layer.DataAccess;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Intepro.BusinessLogic
{
    public class StudentBLL
    {
        public int StudentID { get; set; }
       
        public string Lastname { get; set; }
        
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
            dal.SetSql("DELETE Students WHERE StudentID = @is");
            dal.AddParameter("@id", StudentID);
            dal.Execute();
            dal.Close();
        }
    }
}