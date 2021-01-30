using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class RegisterPublishDocumentMN
    {

        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListRegisterPublishDocument(string PublishDocument, string CreatedBy, string States,string CheckWait,
            string DocumentNo, string DocumentName, string IndexWord,string Department, DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterPublishDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
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

        public DataTable ListRegisterPublishDocumentByType(string PublishDocument, string CreatedBy, string Type,string DCC,
            DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterPublishDocumentByType";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@DCC", DCC);
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

        public int InsertOrUpdateRegisterPublishDocument(string ID, string PublishDocument, string User, string ApplicationSite, DateTime EffectiveDate, string DocumentNo,
            string Rev, string DocumentName, string DocumentType, string RevisionApplication, string CheckingNotice, string DeletedDocumentOld, string ReferenceDocument, string IndexWord,
            string ContentFile, string CodeOfForm, string ApplicableSite, string ApplicableBU, string NeedReleaseFile, string Status, string States, string CheckWait,
            DateTime ApplicationDate,string DepartmentCheck,string CodeDocument,string Department)
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
                    cmd.CommandText = "Insert into RegisterPublishDocument(ID,PublishDocument,CreatedBy,CreatedDate,ApplicationSite,EffectiveDate,DocumentNo,Rev,"
                                    + "DocumentName,DocumentType,RevisionApplication,CheckingNotice,DeletedDocumentOld,ReferenceDocument,IndexWord,ContentFile,"
                                    + "CodeOfForm,ApplicableSite,ApplicableBU,NeedRelease,NeedReleaseFile,IsDeleted,Status,States,CheckWait,ApplicationDate,DepartmentCheck,CodeDocument,Department,Type) "
                                    + "values(NEWID(), @PublishDocument, @CreatedBy, GETDATE(), @ApplicationSite, @EffectiveDate, @DocumentNo, @Rev,"
                                    + "@DocumentName, @DocumentType, @RevisionApplication, @CheckingNotice, @DeletedDocumentOld, @ReferenceDocument, @IndexWord, @ContentFile,"
                                    + "@CodeOfForm, @ApplicableSite, @ApplicableBU, 1,@NeedReleaseFile, 0, @Status, @States, @CheckWait, @ApplicationDate,@DepartmentCheck,@CodeDocument,@Department,'1')"
                                    + " update DCC_Ref set [Status] ='2' where CodeDocument =@CodeDocument  and DocNo =@DocumentNo";
                    cmd.Parameters.Clear();

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
                    cmd.CommandText = "update RegisterPublishDocument set PublishDocument=@PublishDocument,ApplicationSite=@ApplicationSite,EffectiveDate=@EffectiveDate,DocumentNo=@DocumentNo,"
                                    + "Rev = @Rev,DocumentName = @DocumentName,DocumentType = @DocumentType,RevisionApplication = @RevisionApplication,CheckingNotice = @CheckingNotice,"
                                    + "DeletedDocumentOld = @DeletedDocumentOld,ReferenceDocument = @ReferenceDocument,IndexWord = @IndexWord,ContentFile = @ContentFile,CodeOfForm = @CodeOfForm,"
                                    + "ApplicableSite = @ApplicableSite,ApplicableBU = @ApplicableBU,NeedReleaseFile = @NeedReleaseFile,UpdatedDate = GETDATE(),UpdatedBy = @UpdatedBy,"
                                    + "Status = @Status,States = @States,CheckWait = @CheckWait,ApplicationDate = @ApplicationDate,DepartmentCheck=@DepartmentCheck,Department=@Department where ID = @ID";
                    cmd.Parameters.Clear();

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

        public int InsertApprovalSection_PublishDocument(string PublishDocument, string UserName, string DepartmentCheck,string Department)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection_PublishDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
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

        public DataTable CheckDisplaySubmitPublishDocument(string PublishDocument,string LoginName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitPublishDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
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

        public int AcceptRegisterPublishDocument(string PublishDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptRegisterPublishDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
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

        public int RejectRegisterPublishDocument(string PublishDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterPublishDocument set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where PublishDocument = '" + PublishDocument + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckApprovalSection(string PublishDocument,string States)
        {
            try
            {
                DataTable aData = new DataTable();
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckApprovalSection";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
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
        
        public int DeletedRegisterPublishDocument(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterPublishDocument set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'" 
                                + "declare @CodeDocument varchar(50) declare @DocNo varchar(150)"
                                + " select top 1 @CodeDocument=CodeDocument,@DocNo=DocumentNo from RegisterPublishDocument where ID ='" + ID + "'"
                                + " update DCC_Ref set [Status] = '1' where CodeDocument=@CodeDocument and DocNo=@DocNo ";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SearchRegisterPublishDocument(string DocumentNo, string ListDocumentNo)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_SearchRegisterPublishDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@ListDocumentNo", ListDocumentNo);

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

        public DataTable CheckListSendMail_Publish(string PublishDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_Publish";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
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

        public DataTable CheckSendMailAuto()
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckSendMailAuto";
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
        
        public int UpdateRegisterPublishDocumentByPublishDocument(string PublishDocument, string UserEdit, string StatusEdit)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterPublishDocument set UserEdit =@UserEdit,StatusEdit=@StatusEdit where PublishDocument=@PublishDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PublishDocument", PublishDocument);
                cmd.Parameters.AddWithValue("@UserEdit", UserEdit);
                cmd.Parameters.AddWithValue("@StatusEdit", StatusEdit);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CreateCodeFormAuto(string Code_Dcc, string NameCol, string NameTable)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CreateCodeFormAuto";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code_Dcc", Code_Dcc.Trim());
                cmd.Parameters.AddWithValue("@NameCol", NameCol.Trim());
                cmd.Parameters.AddWithValue("@NameTable", NameTable.Trim());
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