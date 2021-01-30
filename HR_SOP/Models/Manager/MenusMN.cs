using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class MenusMN
    {

        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable GetMenus(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetMenus";
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

        public DataTable ListMenus(string Code, string Name)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListMenus";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@Name", Name);
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

        public int InsertOrUpdateMenus(string ID, string Code, string Name, string Url, string User,string Orders,string Groups)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                if (String.IsNullOrEmpty(ID))
                {
                    cmd.CommandText = "insert into Menus(ID,Code,Name,Url,CreatedBy,CreatedDate,IsDeleted,Orders,Groups)"
                                    + "values(NEWID(), @Code, @Name, @Url, @CreatedBy, GETDATE(), 0, @Orders, @Groups)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Url", Url);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                    cmd.Parameters.AddWithValue("@Orders", Orders);
                    cmd.Parameters.AddWithValue("@Groups", Groups);
                }
                else
                {
                    cmd.CommandText = "update Menus set Name=@Name,Url=@Url,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE(),Orders=@Orders,Groups=@Groups where ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Url", Url);
                    cmd.Parameters.AddWithValue("@UpdatedBy", User);
                    cmd.Parameters.AddWithValue("@Orders", Orders);
                    cmd.Parameters.AddWithValue("@Groups", Groups);
                    cmd.Parameters.AddWithValue("@ID", ID);
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

        public int DeletedMenus(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Menus set DeletedBy='" + User + "',DeletedDate=GETDATE(),IsDeleted=1 where ID='" + ID + "'";
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