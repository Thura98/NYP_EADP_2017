using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EAD_Project.DAL
{
    public class MasterPageDAO
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
        public string getName(string NRIC, string role)
        {
            conn.Open();
            string query = "select Name from " + role + " where NRIC='" + NRIC + "'";
            SqlCommand com = new SqlCommand(query, conn);
            string Name = com.ExecuteScalar().ToString();
            return Name;
        }
    }
}