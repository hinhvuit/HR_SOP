using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class ApplicationObsoletedSecurityMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();

        public DataTable ListApplicationObsoletedDocument(string ObsoletedDocument, string CreatedBy, string States, string CheckWait,
            string DocNo, string DocName, string Department, DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListApplicationObsoletedSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ObsoletedDocument", ObsoletedDocument);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                cmd.Parameters.AddWithValue("@DocNo", DocNo);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@FromApplicationDate", FromApplicationDate);
                cmd.Parameters.AddWithValue("@ToApplicationDate", ToApplicationDate);

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

        public int InsertOrUpdateApplicationObsoletedDocument(string ID, string ObsoletedDocument, string PublishDocument,
            DateTime EffectiveDate, string ReasonObsoleted, string States, DateTime ApplicationDate, string Users)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                if (String.IsNullOrEmpty(ID))
                {
                    #region
                    cmd.CommandText = @"insert into ApplicationObsoletedSecurity(ID,ObsoletedDocument,PublishDocument,EffectiveDate,ReasonObsoleted,States,ApplicationDate,CreatedBy,CreatedDate,IsDeleted) 
                                       values(NEWID(),@ObsoletedDocument,@PublishDocument,@EffectiveDate,@ReasonObsoleted,@States,@ApplicationDate,@CreatedBy,GETDATE(),0) 
                                       update RegisterPublishDocument set Type='2' where PublishDocument=@PublishDocument ";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@ObsoletedDocument", ObsoletedDocument);
                    cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@ReasonObsoleted", ReasonObsoleted);
                    cmd.Parameters.AddWithValue("@States", States);

                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", Users);

                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = @"update ApplicationObsoletedSecurity set EffectiveDate=@EffectiveDate,
                                        ReasonObsoleted=@ReasonObsoleted,States=@States,ApplicationDate=@ApplicationDate,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID=@ID";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@ReasonObsoleted", ReasonObsoleted);
                    cmd.Parameters.AddWithValue("@States", States);

                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Users);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    #endregion
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

        public int InsertApprovalSection_ObsoletedDocument(string ObsoletedDocument, string UserName)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection_ObsoletedSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ObsoletedDocument", ObsoletedDocument);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckDisplaySubmitObsoletedDocument(string ObsoletedDocument, string LoginName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitObsoletedSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ObsoletedDocument", ObsoletedDocument);
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
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

        public int AcceptAcceptApplicationObsoletedDocument(string ObsoletedDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptApplicationObsoletedSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ObsoletedDocument", ObsoletedDocument);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RejectApplicationObsoletedDocument(string ObsoletedDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update ApplicationObsoletedSecurity set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where ObsoletedDocument = '" + ObsoletedDocument + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckApprovalSection_ObsoletedDocument(string ObsoleteDocument, string States)
        {
            try
            {
                DataTable aData = new DataTable();
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckApprovalSection_ObsoletedSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ObsoleteDocument", ObsoleteDocument);
                cmd.Parameters.AddWithValue("@States", States);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return Convert.ToInt32(aData.Rows[0]["TS"]);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
        }

        public int DeletedApplicationObsoletedDocument(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @" update ApplicationObsoletedSecurity set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "' "
                                + " update RegisterPublishSecurity set Type='1' where PublishDocument=(select top 1 PublishDocument from ApplicationObsoletedDocument where ID='" + ID + "')";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckListSendMail_Obsoleted(string ObsoletedDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_ObsoletedSe";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ObsoletedDocument", ObsoletedDocument);
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