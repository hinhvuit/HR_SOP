using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class RolesMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListRoles(string Code, string Name, string IsDeleted)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRoles";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
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

        public int InsertOrUpdateRole(string ID, string Code, string Name, string IsDeleted, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                if (String.IsNullOrEmpty(ID))
                {
                    cmd.CommandText = "insert into Roles(ID,Code,Name,CreatedBy,CreatedDate,IsDeleted) values(NEWID(),'"
                        + Code + "','" + Name + "','" + User + "',GETDATE(),'" + IsDeleted + "')";
                }
                else
                {
                    cmd.CommandText = "update Roles set Code='" + Code + "',Name='" + Name + "',UpdatedBy ='"
                        + User + "',UpdatedDate=GETDATE(),IsDeleted ='" + IsDeleted + "' where ID = '" + ID + "'";
                }
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeletedRole(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Roles set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'";
                cmd.ExecuteNonQuery();
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