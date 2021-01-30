using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class UserInDepartmentMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListDepartmentInUser(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListDepartmentInUser";
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

        public int InsertUserInDepartment(string UserName, string Department, string CreatedBy)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete UserInDepartment where UserName =@UserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.ExecuteNonQuery();

                string[] ListDepartment = Department.Split(',');
                for (int i = 0; i < ListDepartment.Length; i++)
                {
                    if (!String.IsNullOrEmpty(ListDepartment[i]))
                    {
                        cmd.CommandText = "insert into UserInDepartment(ID,UserName,Department,CreatedBy,CreatedDate)"
                        + "values(NEWID(), @UserName, @Department, @CreatedBy, GETDATE())";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Department", ListDepartment[i]);
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

        public DataTable ListDepartmentByUserName(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListDepartmentByUserName";
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
    }
}