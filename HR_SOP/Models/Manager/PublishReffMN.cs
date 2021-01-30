using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class PublishReffMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListPublishReff(string PublishDocument, string FormNo, string FormName, string PreservingDepartment,string States, DateTime FromDate, DateTime ToDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListPublishReff";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                cmd.Parameters.AddWithValue("@FormNo", FormNo);
                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@PreservingDepartment", PreservingDepartment);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
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

        public DataTable ListPublishReffByEditDocument(string EditDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListPublishReffByEditDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EditDocument", EditDocument);
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

        public int InsertPublishReff(string PublishDocument, string FormNo, string FormName,
            string PreservingDepartment, DateTime PreservingTime, string Attachment, string Type, string User,int OrderBy)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"insert into PublishReff(ID,PublishDocument,FormNo,FormName,PreservingDepartment,PreservingTime,Attachment,Type,CreatedBy,CreatedDate,IsDeleted,OrderBy)
                                        values(NEWID(),@PublishDocument,@FormNo,@FormName,@PreservingDepartment,@PreservingTime,@Attachment,@Type,@CreatedBy,GETDATE(),0,@OrderBy)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                cmd.Parameters.AddWithValue("@FormNo", FormNo);
                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@PreservingDepartment", PreservingDepartment);
                cmd.Parameters.AddWithValue("@PreservingTime", PreservingTime);

                cmd.Parameters.AddWithValue("@Attachment", Attachment);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@CreatedBy", User);
                cmd.Parameters.AddWithValue("@OrderBy", OrderBy);

                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeletedPublishReff(string PublishDocument)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete PublishReff where PublishDocument = '" + PublishDocument + "'";
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