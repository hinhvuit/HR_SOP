using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class UsersInRolesMN
    {


        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListRolesCheck(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRolesCheck";
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

        public DataTable ListUsersInRoles(string UserName, string CodeRole)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListUsersInRoles";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@CodeRole", CodeRole);
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

        public int InsertUsersInRoles(string UserName, string CodeRole, string CreatedBy)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete UsersInRoles where UserName =@UserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.ExecuteNonQuery();

                string[] ListCodeRole = CodeRole.Split(',');
                for (int i = 0; i < ListCodeRole.Length; i++)
                {
                    if (!String.IsNullOrEmpty(ListCodeRole[i]))
                    {
                        cmd.CommandText = "insert into UsersInRoles(ID,UserName,CodeRole,CreatedBy,CreatedDate)"
                        + "values(NEWID(), @UserName, @CodeRole, @CreatedBy, GETDATE())";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@CodeRole", ListCodeRole[i]);
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