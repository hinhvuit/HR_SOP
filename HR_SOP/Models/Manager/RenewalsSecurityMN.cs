using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class RenewalsSecurityMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();

        public DataTable ListRegisterRenewalDocument(string Code, string DCC, string Type)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterRenewalSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@DCC", DCC);
                cmd.Parameters.AddWithValue("@Type", Type);
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

        public DataTable ListRenewalsDocument(string RenewalCode, string CreatedBy, string States, string CheckWait, string DocumentNo,
            DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRenewalsSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RenewalCode", RenewalCode);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
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

        public int InsertOrUpdateRenewalsDocument(string ID, string RenewalCode, DateTime ApplicationDate, string ApplicationSite, DateTime EffectiveDate,
            string TypeRenewal, string DocumentNo, string DCC_NO, string Reason, string Revisor,
            DateTime CloseDate, string States, string Department, string User, string BeforRevised, string BeforRevisor, string BeforCloseDate, string Type)
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
                    cmd.CommandText = @"insert into RenewalsSecurity(ID,RenewalCode,ApplicationDate,ApplicationSite,EffectiveDate,TypeRenewal,
                                        DocumentNo,DCC_NO,Reason,Revisor,CloseDate,States,Department,CreatedBy,CreatedDate,IsDeleted,BeforRevised,BeforRevisor,BeforCloseDate,Type)
                                        values(NEWID(),@RenewalCode,@ApplicationDate,@ApplicationSite,@EffectiveDate,@TypeRenewal,
                                        @DocumentNo,@DCC_NO,@Reason,@Revisor,@CloseDate,@States,@Department,@CreatedBy,getdate(),0,@BeforRevised,@BeforRevisor,@BeforCloseDate,@Type)";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@RenewalCode", RenewalCode);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@TypeRenewal", TypeRenewal);

                    cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                    cmd.Parameters.AddWithValue("@DCC_NO", DCC_NO);
                    cmd.Parameters.AddWithValue("@Reason", Reason);
                    cmd.Parameters.AddWithValue("@Revisor", Revisor);
                    cmd.Parameters.AddWithValue("@CloseDate", CloseDate);

                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                    cmd.Parameters.AddWithValue("@BeforRevised", BeforRevised);
                    cmd.Parameters.AddWithValue("@BeforRevisor", BeforRevisor);

                    cmd.Parameters.AddWithValue("@BeforCloseDate", BeforCloseDate);
                    cmd.Parameters.AddWithValue("@Type", Type);

                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = @"update RenewalsSecurity set ApplicationSite=@ApplicationSite,TypeRenewal=@TypeRenewal,Reason=@Reason,Revisor=@Revisor,
                                        CloseDate=@CloseDate,States=@States,Department=@Department,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID=@ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@TypeRenewal", TypeRenewal);
                    cmd.Parameters.AddWithValue("@Reason", Reason);
                    cmd.Parameters.AddWithValue("@Revisor", Revisor);

                    cmd.Parameters.AddWithValue("@CloseDate", CloseDate);
                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@Department", Department);
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

        public int DeletedRenewalsDocument(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RenewalsSecurity set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AcceptRenewalsDocument(string RenewalCode, string User, string States, string Person)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptRenewalsSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RenewalCode", RenewalCode);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@Person", Person);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RejectRenewalsDocument(string RenewalCode, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RenewalsSecurity set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where RenewalCode = '" + RenewalCode + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertApprovalSection_Renewal(string RenewalCode, string UserName, string Department)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection_Renewal";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RenewalCode", RenewalCode);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckListSendMail_Renewal(string RenewalCode)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_RenewalSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RenewalCode", RenewalCode);
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

        public DataTable CheckDisplaySubmitRenewalsDocument(string RenewalCode)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitRenewalsSecurity";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RenewalCode", RenewalCode);
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