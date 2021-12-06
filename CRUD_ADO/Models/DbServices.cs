using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_ADO.Models
{
    public class DbServices
    {
        //connecton
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);

        //get data
        public List<EmpModel> GetData()
        {
            List<EmpModel> emplist = new List<EmpModel>();
            SqlCommand cmd = new SqlCommand("select_emp", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                emplist.Add(new EmpModel
                {
                    ID=Convert.ToInt32(dr[0]),
                    NAME=Convert.ToString(dr[1]),
                    EMAIL=Convert.ToString(dr[2])
                });
            }
            return emplist;
        }


        //Add
        public bool Add(EmpModel obj)
        {
            SqlCommand cmd = new SqlCommand("crud",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "add");
            cmd.Parameters.AddWithValue("@id", obj.ID);
            cmd.Parameters.AddWithValue("@name", obj.NAME);
            cmd.Parameters.AddWithValue("@email", obj.EMAIL);

            if (con.State == ConnectionState.Closed)
                con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //Upadte
        public bool Update(EmpModel obj)
        {
            SqlCommand cmd = new SqlCommand("crud", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "Update");
            cmd.Parameters.AddWithValue("@id", obj.ID);
            cmd.Parameters.AddWithValue("@name", obj.NAME);
            cmd.Parameters.AddWithValue("@email", obj.EMAIL);

            if (con.State == ConnectionState.Closed)
                con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }




        //Delete
        public bool Del(EmpModel obj)
        {
            SqlCommand cmd = new SqlCommand("crud", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "delete");
            cmd.Parameters.AddWithValue("@id", obj.ID);

            if (con.State == ConnectionState.Closed)
                con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}