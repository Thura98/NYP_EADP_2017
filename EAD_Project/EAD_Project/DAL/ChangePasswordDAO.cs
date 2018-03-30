using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EAD_Project.DAL
{
    public class ChangePasswordDAO
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);

        public bool checkUser(string guid)
        {
            bool Exist = false;
            conn.Open();
            string checkExist = "select count(*) from tblResetPasswordRequests where Id='" + guid + "'";
            SqlCommand com = new SqlCommand(checkExist, conn);
            int Check = Convert.ToInt32(com.ExecuteScalar().ToString());
            if (Check == 1)
            {
                Exist = true;
            }
            conn.Close();
            return Exist;
        }

       public void changePassword(string guid, string password, string role)
       {
            conn.Open();
            string getUserName = "Select NRIC from tblResetPasswordRequests where Id='" + guid + "'";
            SqlCommand com = new SqlCommand(getUserName, conn);
            string NRIC = com.ExecuteScalar().ToString();

            string resetPass = "Update "+ role + " set Password=@Password where NRIC='" + NRIC + "'";
            SqlCommand ResetCom = new SqlCommand(resetPass, conn);
            ResetCom.Parameters.AddWithValue("@Password", password);
            ResetCom.ExecuteNonQuery();

            string deleteRecord = "Delete from tblResetPasswordRequests where Id='" + guid + "'";
            SqlCommand DeleteCom = new SqlCommand(deleteRecord, conn);
            DeleteCom.ExecuteNonQuery();

            conn.Close();
       }

        public void deleteSameRecord()
        {
            conn.Open();
            string query1 = "With ResetPasswordRequestsCTE As (Select *, ROW_NUMBER() Over(Partition By NRIC order by ResetRequestDateTime desc) as RowNumber from tblResetPasswordRequests) Delete from ResetPasswordRequestsCTE where RowNumber > 1";
            SqlCommand com = new SqlCommand(query1, conn);
            com.ExecuteNonQuery();
            conn.Close();
        }
    }
}