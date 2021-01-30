using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class CheckingNoticeMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();

        public DataTable ListRegisterCheckingNotice(string PublishDocument, string CreatedBy, string DocumentNo, DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterCheckingNotice";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
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

        public DataTable ListCheckingNotice(string CodeCheck, string CreatedBy, string States,string CheckWait,string Person, DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListCheckingNotice";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeCheck", CodeCheck);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                cmd.Parameters.AddWithValue("@Person", Person);
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
        
        public int InsertOrUpdateCheckingNotice(string ID, string CodeCheck, string PublishDocument, string DocumentNo,
            DateTime ApplicationDate, string Department, string Director, string States,string User)
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
                    cmd.CommandText = @"insert into CheckingNotice(ID,CodeCheck,PublishDocument,DocumentNo,ApplicationDate,Department,Director,States,CreatedBy,CreatedDate,IsDeleted) 
                                                    values(NEWID(),@CodeCheck,@PublishDocument,@DocumentNo,@ApplicationDate,@Department,@Director,@States,@CreatedBy,getdate(),0)";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@CodeCheck", CodeCheck);
                    cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                    cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);

                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@Director", Director);
                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                    
                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = @"update CheckingNotice set Department=@Department,Director=@Director,States=@States,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID=@ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@Director", Director);
                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@UpdatedBy", User);
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
        
        public int DeletedCheckingNotice(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CheckingNotice set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int AcceptCheckingNotice(string CodeCheck, string User, string States,string Opinion, string Person,DateTime EstimateCloseDate,string Reason)
        {
            try
            {
                
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptCheckingNotice";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeCheck", CodeCheck);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@Opinion", Opinion);
                cmd.Parameters.AddWithValue("@Person", Person);
                cmd.Parameters.AddWithValue("@EstimateCloseDate", EstimateCloseDate);
                cmd.Parameters.AddWithValue("@Reason", Reason);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int RejectCheckingNotice(string CodeCheck, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CheckingNotice set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where CodeCheck = '" + CodeCheck + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int InsertApprovalSection_Checking(string CodeCheck, string UserName)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection_Checking";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeCheck", CodeCheck);
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
        
        public DataTable CheckListSendMail_Checking(string CodeCheck)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_Checking";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeCheck", CodeCheck);
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

        public DataTable CheckDisplaySubmitCheckingNotice(string CodeCheck, string LoginName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitCheckingNotice";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeCheck", CodeCheck);
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

    }
}