using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class UserInPositionMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListPositionInUser(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListPositionInUser";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckPosition(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from UserInPosition where UserName =@UserName and Position in ('C-00004','C-00008')";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertUserInPosition(string UserName, string Position, string CreatedBy)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete UserInPosition where UserName =@UserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.ExecuteNonQuery();

                string[] ListPosition = Position.Split(',');
                for (int i = 0; i < ListPosition.Length; i++)
                {
                    if (!String.IsNullOrEmpty(ListPosition[i]))
                    {
                        cmd.CommandText = "insert into UserInPosition(ID,UserName,Position,CreatedBy,CreatedDate)"
                        + "values(NEWID(), @UserName, @Position, @CreatedBy, GETDATE())";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Position", ListPosition[i]);
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd.ExecuteNonQuery();
                    }
                }
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}