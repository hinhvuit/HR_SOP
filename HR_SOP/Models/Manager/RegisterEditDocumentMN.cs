using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class RegisterEditDocumentMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();

        public DataTable ListRegisterEditDocument(string EditDocument, string CreatedBy, string States, string CheckWait,
            string DocumentNo, string DocumentName, string IndexWord,string Department, DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterEditDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EditDocument", EditDocument);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);

                cmd.Parameters.AddWithValue("@DocumentName", DocumentName);
                cmd.Parameters.AddWithValue("@IndexWord", IndexWord);
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
        
        public int InsertOrUpdateRegisterEditDocument(string ID,string EditDocument, string PublishDocument, string User, string ApplicationSite, DateTime EffectiveDate, string DocumentNo,
            string Rev, string DocumentName, string DocumentType, string RevisionApplication, string CheckingNotice, string DeletedDocumentOld, string ReferenceDocument, string IndexWord,
            string ContentFile, string CodeOfForm, string ApplicableSite, string ApplicableBU, string NeedReleaseFile, string Status, string States, string CheckWait,
            DateTime ApplicationDate, string DepartmentCheck, string CodeDocument, string Department)
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
                    cmd.CommandText = "Insert into RegisterEditDocument(ID,EditDocument,PublishDocument,CreatedBy,CreatedDate,ApplicationSite,EffectiveDate,DocumentNo,Rev,"
                                    + "DocumentName,DocumentType,RevisionApplication,CheckingNotice,DeletedDocumentOld,ReferenceDocument,IndexWord,ContentFile,"
                                    + "CodeOfForm,ApplicableSite,ApplicableBU,NeedRelease,NeedReleaseFile,IsDeleted,Status,States,CheckWait,ApplicationDate,DepartmentCheck,CodeDocument,Department,Type) "
                                    + "values(NEWID(),@EditDocument, @PublishDocument, @CreatedBy, GETDATE(), @ApplicationSite, @EffectiveDate, @DocumentNo, @Rev,"
                                    + "@DocumentName, @DocumentType, @RevisionApplication, @CheckingNotice, @DeletedDocumentOld, @ReferenceDocument, @IndexWord, @ContentFile,"
                                    + "@CodeOfForm, @ApplicableSite, @ApplicableBU, 1,@NeedReleaseFile, 0, @Status, @States, @CheckWait, @ApplicationDate,@DepartmentCheck,@CodeDocument,@Department,'1')"
                                    + " update RegisterPublishDocument set Type='3' where PublishDocument=@PublishDocument";
                                    //+ " update CheckingNotice set States='F15' where PublishDocument=@PublishDocument";

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@EditDocument", EditDocument);

                    cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);

                    cmd.Parameters.AddWithValue("@Rev", Rev);
                    cmd.Parameters.AddWithValue("@DocumentName", DocumentName);
                    cmd.Parameters.AddWithValue("@DocumentType", DocumentType);
                    cmd.Parameters.AddWithValue("@RevisionApplication", RevisionApplication);
                    cmd.Parameters.AddWithValue("@CheckingNotice", CheckingNotice);

                    cmd.Parameters.AddWithValue("@DeletedDocumentOld", DeletedDocumentOld);
                    cmd.Parameters.AddWithValue("@ReferenceDocument", ReferenceDocument);
                    cmd.Parameters.AddWithValue("@IndexWord", IndexWord);
                    cmd.Parameters.AddWithValue("@ContentFile", ContentFile);
                    cmd.Parameters.AddWithValue("@CodeOfForm", CodeOfForm);

                    cmd.Parameters.AddWithValue("@ApplicableSite", ApplicableSite);
                    cmd.Parameters.AddWithValue("@ApplicableBU", ApplicableBU);
                    cmd.Parameters.AddWithValue("@NeedReleaseFile", NeedReleaseFile);
                    cmd.Parameters.AddWithValue("@Status", Status);

                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@DepartmentCheck", DepartmentCheck);
                    cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = "update RegisterEditDocument set EditDocument=@EditDocument, PublishDocument=@PublishDocument,ApplicationSite=@ApplicationSite,EffectiveDate=@EffectiveDate,DocumentNo=@DocumentNo,"
                                    + "Rev = @Rev,DocumentName = @DocumentName,DocumentType = @DocumentType,RevisionApplication = @RevisionApplication,CheckingNotice = @CheckingNotice,"
                                    + "DeletedDocumentOld = @DeletedDocumentOld,ReferenceDocument = @ReferenceDocument,IndexWord = @IndexWord,ContentFile = @ContentFile,CodeOfForm = @CodeOfForm,"
                                    + "ApplicableSite = @ApplicableSite,ApplicableBU = @ApplicableBU,NeedReleaseFile = @NeedReleaseFile,UpdatedDate = GETDATE(),UpdatedBy = @UpdatedBy,"
                                    + "Status = @Status,States = @States,CheckWait = @CheckWait,ApplicationDate = @ApplicationDate,DepartmentCheck=@DepartmentCheck,Department=@Department where ID = @ID";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@EditDocument", EditDocument);

                    cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                    cmd.Parameters.AddWithValue("@UpdatedBy", User);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);

                    cmd.Parameters.AddWithValue("@Rev", Rev);
                    cmd.Parameters.AddWithValue("@DocumentName", DocumentName);
                    cmd.Parameters.AddWithValue("@DocumentType", DocumentType);
                    cmd.Parameters.AddWithValue("@RevisionApplication", RevisionApplication);
                    cmd.Parameters.AddWithValue("@CheckingNotice", CheckingNotice);

                    cmd.Parameters.AddWithValue("@DeletedDocumentOld", DeletedDocumentOld);
                    cmd.Parameters.AddWithValue("@ReferenceDocument", ReferenceDocument);
                    cmd.Parameters.AddWithValue("@IndexWord", IndexWord);
                    cmd.Parameters.AddWithValue("@ContentFile", ContentFile);
                    cmd.Parameters.AddWithValue("@CodeOfForm", CodeOfForm);

                    cmd.Parameters.AddWithValue("@ApplicableSite", ApplicableSite);
                    cmd.Parameters.AddWithValue("@ApplicableBU", ApplicableBU);
                    cmd.Parameters.AddWithValue("@NeedReleaseFile", NeedReleaseFile);
                    cmd.Parameters.AddWithValue("@Status", Status);

                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    cmd.Parameters.AddWithValue("@DepartmentCheck", DepartmentCheck);
                    cmd.Parameters.AddWithValue("@Department", Department);
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

        public int InsertApprovalSection_EditDocument(string EditDocument, string UserName, string DepartmentCheck, string Department)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection_EditDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EditDocument", EditDocument);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@DepartmentCheck", DepartmentCheck);
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

        public DataTable CheckDisplaySubmitEditDocument(string EditDocument, string LoginName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitEditDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EditDocument", EditDocument);
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

        public int AcceptRegisterEditDocument(string EditDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptRegisterEditDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EditDocument", EditDocument);
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

        public int RejectRegisterEditDocument(string EditDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterEditDocument set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where EditDocument = '" + EditDocument + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckApprovalSection_EditDocument(string EditDocument,string States)
        {
            try
            {
                DataTable aData = new DataTable();
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckApprovalSection_EditDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EditDocument", EditDocument);
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

        public int DeletedRegisterEditDocument(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterEditDocument set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'"
                                + "declare @PublishDocument varchar(50)"
                                + " select top 1 @PublishDocument=PublishDocument  from RegisterEditDocument where ID ='" + ID + "'"
                                + " update RegisterPublishDocument set [Type] = '1' where PublishDocument=@PublishDocument";
                                //+ " update CheckingNotice set States = 'F10' where PublishDocument=@PublishDocument";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable CheckListSendMail_Edit(string EditDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_Edit";
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
        
        public DataTable GetNextAlphabet(string Word)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetNextAlphabet";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Word", Word);
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