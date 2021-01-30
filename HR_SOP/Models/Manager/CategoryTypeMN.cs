using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class CategoryTypeMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();

        public DataTable ListCategoryType()
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from CategoryType where IsDeleted = 0 order by  Orders";
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

        public int InsertOrUpdateCategoryType(string ID, string CatTypeCode, string CatTypeName, string Orders, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                if (String.IsNullOrEmpty(ID))
                {
                    cmd.CommandText = "insert into CategoryType(ID,CatTypeCode,CatTypeName,Orders,CreatedBy,CreatedDate,IsDeleted) "
                                    + "values(NEWID(), @CatTypeCode, @CatTypeName, @Orders, @CreatedBy, GETDATE(), 0)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CatTypeCode", CatTypeCode);
                    cmd.Parameters.AddWithValue("@CatTypeName", CatTypeName);
                    cmd.Parameters.AddWithValue("@Orders", Orders);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                }
                else
                {
                    cmd.CommandText = "update CategoryType set CatTypeName=@CatTypeName,Orders=@Orders,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID =@ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CatTypeName", CatTypeName);
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

        public int DeletedCategoryType(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CategoryType set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'";
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