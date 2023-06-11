using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
using MVC_CRUD.Models;
using System.Data; // CommandType

namespace MVC_CRUD.DataAccess
{
    public class DAL
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString);

        public List<UserRegModel> Get_DataList()
        {
            List<UserRegModel> lst = new List<UserRegModel>();

            SqlCommand cmd = new SqlCommand("spSelect", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new UserRegModel
                {
                    id = Convert.ToInt32(dr[0]),
                    emailid = Convert.ToString(dr[1]),
                    password = Convert.ToString(dr["password"]),
                    name = Convert.ToString(dr[3])
                });
            }

            return lst;
        }

        public bool InsertData(UserRegModel ur)
        {
            int i;
            SqlCommand cmd = new SqlCommand("spInsert ", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", ur.emailid);
            cmd.Parameters.AddWithValue("@pwd", ur.password);
            cmd.Parameters.AddWithValue("@nm", ur.name);
            connection.Open();
            i = cmd.ExecuteNonQuery();
            connection.Close();
            
            if (i >=1)
            {
                // if at least 1 row gets inside i, then i > 1.
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}