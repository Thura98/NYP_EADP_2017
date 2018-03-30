using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace EAD_Project.DAL
{
    public class LoginDAO
    {   
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
        LoginObject L = new LoginObject();
        public LoginObject UserLogin(string role, string UserName)
        {
            
            conn.Open();
            string checkuser = "select count(*) from " + role + " where NRIC='" + UserName + "'";
            SqlCommand com = new SqlCommand(checkuser, conn);
            L.User = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (L.User == 1)
            {
                conn.Open();
                string checkPasswordQuery = "select password from " + role + " where NRIC='" + UserName + "'";
                SqlCommand passCom = new SqlCommand(checkPasswordQuery, conn);
                L.password = passCom.ExecuteScalar().ToString();
            }
            conn.Close();

            return L;
        }

        public LoginObject resetPassword(string Email, string role)
        {
            conn.Open();
            string checkEmail = "select count(*) from " + role + " where CONVERT(VARCHAR, Email) ='" + Email + "'";
            SqlCommand com = new SqlCommand(checkEmail, conn);
            L.User = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (L.User == 1)
            {
                conn.Open();
                string getUserName = "select NRIC from " + role + " where CONVERT(VARCHAR, Email) = '" + Email + "'";
                SqlCommand UserCom = new SqlCommand(getUserName, conn);
                string username = UserCom.ExecuteScalar().ToString();
                L.UserName = UserCom.ExecuteScalar().ToString();

                Guid guid = Guid.NewGuid();
                DateTime Currenttime = DateTime.Now;
                string resetPassword = "Insert into tblResetPasswordRequests (Id, NRIC, ResetRequestDateTime) values (@paraUniqueID, @paraUserName, @paraCurrentDate)";
                SqlCommand InsertCMD = new SqlCommand(resetPassword, conn);
                InsertCMD.Parameters.AddWithValue("@paraUniqueID", guid);
                InsertCMD.Parameters.AddWithValue("@paraUserName", username);
                InsertCMD.Parameters.AddWithValue("@paraCurrentDate", Currenttime);
                InsertCMD.ExecuteNonQuery();
                L.UniqueID = guid;
            }
            conn.Close();

            return L;
        }


    }
}