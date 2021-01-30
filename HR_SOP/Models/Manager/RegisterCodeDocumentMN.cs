using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class RegisterCodeDocumentMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListRegisterCodeDocument(string CodeDocument, string CreatedBy, string States, string CheckWait,string Type,
        string Department, string DocNo, string DocName, DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterCodeDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@States", States);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                cmd.Parameters.AddWithValue("@Type", Type);

                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@DocNo", DocNo);
                cmd.Parameters.AddWithValue("@DocName", DocName);

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

        public int InsertOrUpdateRegisterCodeDocument(string ID, string CodeDocument, string User, string ApplicationSite, DateTime EffectiveDate,
            string DocumentType, string ReasonApplication, string ApplicableSite, string ApplicableBU, DateTime ApplicationDate, string States,
            string Status, string Department)
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
                    cmd.CommandText = " insert into RegisterCodeDocument(ID,CodeDocument,CreatedDate,CreatedBy,ApplicationSite,EffectiveDate,DocumentType,"
                        + "ReasonApplication,ApplicableSite,ApplicableBU,IsDeleted,Status,States,Department,ApplicationDate,Type) "
                        + "values(NEWID(), @CodeDocument, GETDATE(), @CreatedBy, @ApplicationSite, @EffectiveDate, @DocumentType, @ReasonApplication,"
                        + "@ApplicableSite, @ApplicableBU, 0, @Status, @States, @Department, @ApplicationDate,'1')";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                    cmd.Parameters.AddWithValue("@CreatedBy", User);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@DocumentType", DocumentType);

                    cmd.Parameters.AddWithValue("@ReasonApplication", ReasonApplication);
                    cmd.Parameters.AddWithValue("@ApplicableSite", ApplicableSite);
                    cmd.Parameters.AddWithValue("@ApplicableBU", ApplicableBU);
                    cmd.Parameters.AddWithValue("@Status", Status);

                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = " update RegisterCodeDocument set ApplicationSite=@ApplicationSite,EffectiveDate=@EffectiveDate,DocumentType=@DocumentType,"
                        + "ReasonApplication=@ReasonApplication,ApplicableSite=@ApplicableSite,ApplicableBU=@ApplicableBU,UpdatedDate=GETDATE(),"
                        + "UpdatedBy=@UpdatedBy,Status=@Status,States=@States,Department=@Department,ApplicationDate=@ApplicationDate where ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@UpdatedBy", User);
                    cmd.Parameters.AddWithValue("@ApplicationSite", ApplicationSite);
                    cmd.Parameters.AddWithValue("@EffectiveDate", EffectiveDate);
                    cmd.Parameters.AddWithValue("@DocumentType", DocumentType);

                    cmd.Parameters.AddWithValue("@ReasonApplication", ReasonApplication);
                    cmd.Parameters.AddWithValue("@ApplicableSite", ApplicableSite);
                    cmd.Parameters.AddWithValue("@ApplicableBU", ApplicableBU);
                    cmd.Parameters.AddWithValue("@Status", Status);

                    cmd.Parameters.AddWithValue("@States", States);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
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

        public int DeletedRegisterCodeDocument(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterCodeDocument set IsDeleted = 1,DeletedBy='" + User + "',DeletedDate=GETDATE() where ID = '" + ID + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int CheckApprovalSection_Code(string CodeDocument, string States)
        {
            try
            {
                DataTable aData = new DataTable();
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckApprovalSection_Code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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



        public int AcceptRegisterCodeDocument(string CodeDocument, string User,string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AcceptRegisterCodeDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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

        public int RejectRegisterCodeDocument(string CodeDocument, string User, string States)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update RegisterCodeDocument set States ='" + States + "',UpdatedBy='" + User + "',UpdatedDate=GETDATE() where CodeDocument = '" + CodeDocument + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListDocumentRef(string CodeDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListDocumentRef";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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

        public int InsertDocumentRef(string DocumentName, string FileName, string AssignedRevisor, DateTime EstimatedCloseDate,
            string User, string CodeDocument,int OrderBy)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into DocumentRef(ID,DocumentName,FileName,AssignedRevisor,EstimatedCloseDate,CreatedDate,Createdby,CodeDocument,OrderBy) "
                + "values(NEWID(), @DocumentName, @FileName, @AssignedRevisor, @EstimatedCloseDate, GETDATE(), @Createdby, @CodeDocument, @OrderBy)";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@DocumentName", DocumentName);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@AssignedRevisor", AssignedRevisor);
                cmd.Parameters.AddWithValue("@EstimatedCloseDate", EstimatedCloseDate);
                cmd.Parameters.AddWithValue("@Createdby", User);

                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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

        public int DeleteDocumentRef(string CodeDocument)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete DocumentRef where CodeDocument =@CodeDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ListApprovalSection(string CodeDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListApprovalSection";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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
        
        public int InsertApprovalSection(string CodeDocument, string UserName,string Department)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertApprovalSection";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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
        
        public int UpdateApprovalSection(string CodeDocument, string UserName, string Comment,string Status)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateApprovalSection";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckDisplaySubmitCodeDocument(string CodeDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckDisplaySubmitCodeDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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

        public DataTable CheckListSendMail_Code(string CodeDocument)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckListSendMail_Code";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
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

        public DataTable ListDCC_Ref(string CodeDocument, string Status)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListDCC_Ref";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.Parameters.AddWithValue("@Status", Status);
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

        public DataTable CheckExistDocNo(string DocNo)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckExistDocNo";
                cmd.Parameters.Clear();
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

        public int InsertDCC_Ref(string DocNo, string DocName,string CreatedBy,string CodeDocument,int OrderBy,string ID_DocumentRef)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into DCC_Ref(ID,DocNo,DocName,CreatedBy,CreatedDate,IsDeleted,Status,CodeDocument,OrderBy,ID_DocumentRef) "
                                + "values(NEWID(), @DocNo, @DocName, @CreatedBy, GETDATE(), 0, '1', @CodeDocument, @OrderBy,@ID_DocumentRef)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DocNo", DocNo);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                cmd.Parameters.AddWithValue("@ID_DocumentRef", ID_DocumentRef);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListRegisterCodeDocumentByDCC(string CodeDocument, string CreatedBy,string DocNo, string StatusDCC,
            DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListRegisterCodeDocumentByDCC";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeDocument", CodeDocument);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@DocNo", DocNo);
                cmd.Parameters.AddWithValue("@StatusDCC", StatusDCC);
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

        public DataTable ListSearchDocument(string DocName, string CreatedBy, string DocNo, string StatusDCC,
            DateTime FromApplicationDate, DateTime ToApplicationDate)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListSearchDocument";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@DocNo", DocNo);
                cmd.Parameters.AddWithValue("@StatusDCC", StatusDCC);
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

        public DataTable ListCheckWait(string Code, string Dcc, string Department, string CheckWait,string CreatedBy,
            DateTime FromDate, DateTime ToDate,string Type)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListCheckWait";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@Dcc", Dcc);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@CheckWait", CheckWait);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@FromDate", FromDate); 
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
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
    }
}