using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Layer.DataAccess;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Intepro.BusinessLogic
{
    public class AlbumBLL
    {
        public int AlbumID { get; set; }
        [Required] 
        public string AlbumName { get; set; }
        [Required]
        public string Artists { get; set; }
        [Range(1900, 2021, ErrorMessage = "Year must published within 1900 - 2021")]
        public int YearReleased { get; set; }

        private MssqlDAL dal = new MssqlDAL();

        public void Add()
        {
            dal.Open();
            dal.SetSql("INSERT INTO Albums " + "(AlbumName, Artists, YearReleased)" +
                "VALUES (@a, @b, @c)");
            dal.AddParameter("@a", AlbumName);
            dal.AddParameter("@b", Artists);
            dal.AddParameter("@c", YearReleased);
            dal.Execute();
            dal.Close();
        }
        public List<AlbumBLL> GetAll()
        {
            List<AlbumBLL> list = new List<AlbumBLL>();
            dal.Open();
            dal.SetSql("SELECT * FROM Albums");
            SqlDataReader dr = dal.GetReader();
            while(dr.Read() ==  true)
            {
                AlbumBLL album = new AlbumBLL();
                album.AlbumID = (int)dr["AlbumID"];
                album.AlbumName = dr["AlbumName"].ToString();
                album.Artists = dr["Artists"].ToString();
                album.YearReleased = (int)dr["YearReleased"];

                list.Add(album); ;
            }
            dr.Close();
            dal.Close();
            return list;
        }
        public AlbumBLL Get(int aID)
        {
            AlbumBLL album = new AlbumBLL();

            dal.Open();
            dal.SetSql("SELECT * FROM Albums WHERE AlbumID = @aID");
            dal.AddParameter("@aID", aID);
            SqlDataReader dr = dal.GetReader();
            if (dr.Read() == true)
            {
                album.AlbumID = (int)dr[0];
                album.AlbumName = dr[1].ToString();
                album.Artists = dr[2].ToString();
                album.YearReleased = (int)dr[3];
            }
            dr.Close();
            dal.Close();
            return album;
        }
        public void Edit()
        {
            dal.Open();
            dal.SetSql("UPDATE Albums " +
                "SET    AlbumName = @a, " +
                "       Artists = @b, " +
                "       YearReleased = @c " + 
                "WHERE AlbumID = @aID");
            dal.AddParameter("@a", AlbumName);
            dal.AddParameter("@b", Artists);
            dal.AddParameter("@c", YearReleased);
            dal.AddParameter("@aID", AlbumID);
            dal.Execute();
            dal.Close();
        }
        public void Delete()
        {
            dal.Open();
            dal.SetSql("DELETE Albums WHERE AlbumID = @aID");
            dal.AddParameter("@aID", AlbumID);
            dal.Execute();
            dal.Close();
        }
    }
}