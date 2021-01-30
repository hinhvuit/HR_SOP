using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class RegisterCancelDocumentMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListRegisterCancelDocument(string CancelDocument,string DocNo_DCC,string DocName, string CreatedBy,
            string States, string CheckWait, string Type, string Department,
            DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterCancelDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
                cmd.Parameters.AddWithValue("@DocNo_DCC", DocNo_DCC);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                cmd.Parameters.AddWithValue("@Type", Type);
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

        public int InsertOrUpdateRegisterCancelDocument(string ID, string CancelDocument,DateTime ApplicationDate,DateTime EffectiveDate,string ApplicationSite,
            string CloseDocument,string ApplicationNo_Code,string DocNo_DCC,string ReasonOfApplication,string States,string User,string Department)
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
                    cmd.CommandText = " insert into "
                                    + " RegisterCancelDocument(ID, CancelDocument, ApplicationDate, EffectiveDate, ApplicationSite, CloseDocument, "
                                    + " ApplicationNo_Code, DocNo_DCC, ReasonOfApplication, States, CreatedBy, CreatedDate, IsDeleted, Department) "
                                    + " values(NEWID(), @CancelDocument, @ApplicationDate, @EffectiveDate, @ApplicationSite, @CloseDocument, "
                                    + " @ApplicationNo_Code, @DocNo_DCC, @ReasonOfApplication, @States, @CreatedBy, GETDATE(), 0, @Department)"
                                    + " declare @CodeDocument varchar(50)"
                                    + " select top 1 @CodeDocument=CodeDocument from RegisterCodeDocument where CodeDocument=@ApplicationNo_Code"
                                    + " update DCC_Ref set [Status] ='2' where CodeDocument =@CodeDocument  and DocNo =@DocNo_DCC";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@CloseDocument", CloseDocument);

                    cmd.Parameters.AddWithValue("@ApplicationNo_Code", ApplicationNo_Code);
                    cmd.Parameters.AddWithValue("@DocNo_DCC", DocNo_DCC);
                    cmd.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplication);
                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);

                    cmd.Parameters.AddWithValue("@Department", Department);
                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = "update RegisterCancelDocument set CancelDocument=@CancelDocument,ApplicationDate=@ApplicationDate, "
                                    + " EffectiveDate = @EffectiveDate,ApplicationSite = @ApplicationSite,CloseDocument = @CloseDocument, "
                                    + " ApplicationNo_Code = @ApplicationNo_Code,DocNo_DCC = @DocNo_DCC,ReasonOfApplication = @ReasonOfApplication, "
                                    + " States = @States,UpdatedBy = @UpdatedBy,UpdatedDate = getdate(),Department = @Department where ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);

                    cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@CloseDocument", CloseDocument);

                    cmd.Parameters.AddWithValue("@ApplicationNo_Code", ApplicationNo_Code);
                    cmd.Parameters.AddWithValue("@DocNo_DCC", DocNo_DCC);
                    cmd.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplication);
                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@UpdatedBy", User);

                    cmd.Parameters.AddWithValue("@Department", Department);
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
        
        public int DeletedRegisterCancelDocument(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterCancelDocument set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'"
                                + " declare @DocNo_DCC varchar(150) declare @CodeDocument varchar(50)"
                                + " select top 1 @CodeDocument=ApplicationNo_Code,@DocNo_DCC=DocNo_DCC from RegisterCancelDocument where ID ='" + ID + "'"
                                + " update DCC_Ref set [Status] = '1' where CodeDocument=@CodeDocument and DocNo=@DocNo_DCC ";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AcceptRegisterCancelDocument(string CancelDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptRegisterCancelDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
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

        public int RejectRegisterCancelDocument(string CancelDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterCancelDocument set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where CancelDocument = '" + CancelDocument + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int InsertApprovalSection_CancelDocument(string CancelDocument, string UserName, string Department)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection_CancelDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
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
        
        public DataTable CheckListSendMail_Cancel(string CancelDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_Cancel";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
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

        public DataTable sp_CheckDisplaySubmitCancelDocument(string CancelDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitCancelDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
                cmd.Parameters.AddWithValue("@LoginName", LoginSession.UserName());
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


        public DataTable ListDocmentRefByCodeDocumentAndCodeDCC(string CodeDocument, string DocNo)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListDocmentRefByCodeDocumentAndCodeDCC";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.Parameters.AddWithValue("@DocNo", DocNo);
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

        public int CheckApprovalSection_Cancel(string CancelDocument, string States)
        {
            try
            {
                DataTable aData = new DataTable();
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckApprovalSection_Cancel";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CancelDocument", CancelDocument);
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

    }
}