using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class CategorysMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListCategorys(string CatCode, string CatName, string CatTypeCode)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListCategorys";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CatCode", CatCode);
                cmd.Parameters.AddWithValue("@CatName", CatName);
                cmd.Parameters.AddWithValue("@CatTypeCode", CatTypeCode);
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

        public DataTable GetCategorysByCatTypeCode(string CatTypeCode)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Categorys where CatTypeCode =@CatTypeCode and IsDeleted =0 order by Orders";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CatTypeCode", CatTypeCode);
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

        public DataTable GetCategorysByCode(string Code)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Categorys where Code =@Code and IsDeleted =0 order by Orders";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code", Code);
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

        public int InsertOrUpdateCategorys(string ID, string CatCode, string CatName, string CatTypeCode,string Orders, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                if (String.IsNullOrEmpty(ID))
                {
                    cmd.CommandText = "insert into Categorys(ID,CatCode,CatName,CatTypeCode,Orders,CreatedBy,CreatedDate,IsDeleted)" 
                                       +"values(NEWID(), @CatCode, @CatName, @CatTypeCode, @Orders, @CreatedBy, GETDATE(), 0)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CatCode", CatCode);
                    cmd.Parameters.AddWithValue("@CatName", CatName);
                    cmd.Parameters.AddWithValue("@CatTypeCode", CatTypeCode);
                    cmd.Parameters.AddWithValue("@Orders", Orders);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                }
                else
                {
                    cmd.CommandText = "update Categorys set CatName=@CatName,Orders=@Orders,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID =@ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CatName", CatName);
                    cmd.Parameters.AddWithValue("@Orders", Orders);
                    cmd.Parameters.AddWithValue("@UpdatedBy", User);
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

        public int DeletedCategorys(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Categorys set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'";
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