using HR_SOP.Models;
using HR_SOP.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_SOP
{
    public partial class RegisterEditDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00023"))
                        {
                            Response.Redirect("NoPermitsion.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx?Url=" + Request.Url.PathAndQuery);
                }
                hidUserName.Value = LoginSession.UserName();

                #region
                UserInDepartmentMN aUserInDepartmentMN = new UserInDepartmentMN();
                
                CategorysMN aCategorysMN = new CategorysMN();
                ListItem aListItem = new ListItem();
                string CatTypeCode = string.Empty;
                CatTypeCode = "CT-00003"; //Application Site
                ddlApplicationSite.Items.Clear();
                ddlApplicationSite.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlApplicationSite.DataValueField = "CatCode";
                ddlApplicationSite.DataTextField = "CatName";
                ddlApplicationSite.DataBind();

                //CatTypeCode = "CT-00004"; //Doc Type
                //ddlDocType.Items.Clear();
                //ddlDocType.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                //ddlDocType.DataValueField = "CatCode";
                //ddlDocType.DataTextField = "CatName";
                //ddlDocType.DataBind();

               

                CatTypeCode = "CT-00002";
                ddlPreservingDepartment.Items.Clear();
                ddlPreservingDepartment.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlPreservingDepartment.DataValueField = "CatCode";
                ddlPreservingDepartment.DataTextField = "CatName";
                ddlPreservingDepartment.DataBind();
                ddlPreservingDepartment.Items.Insert(0, new ListItem("---ALL---", "ALL"));

                #endregion

                #region
                hidStatus.Value = Convert.ToString(Request.QueryString["Type"]);
                string EditDocument = Convert.ToString(Request.QueryString["EditDocument"]);
                string PublishDocument = Convert.ToString(Request.QueryString["PublishDocument"]);



                RegisterPublishDocumentMN aRegisterPublishDocumentMN = new RegisterPublishDocumentMN();
                RegisterEditDocumentMN aRegisterEditDocumentMN = new RegisterEditDocumentMN();
                if (!String.IsNullOrEmpty(PublishDocument))
                {
                    #region

                    ddlDepartment.Items.Clear();
                    ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(LoginSession.UserName());
                    ddlDepartment.DataValueField = "Department";
                    ddlDepartment.DataTextField = "NameDepartment";
                    ddlDepartment.DataBind();


                    DataTable aData = aRegisterPublishDocumentMN.ListRegisterPublishDocument(PublishDocument, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                    txtEffectiveDate.Text = string.Empty;
                    txtApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    if (aData.Rows.Count > 0)
                    {
                        txtApplicationName.Text = LoginSession.FullName();
                        hidPublishDocument.Value = PublishDocument;
                        hidCodeDocument.Value = Convert.ToString(aData.Rows[0]["CodeDocument"]);
                        txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["PublishDocument"]);
                        ddlApplicationSite.Text = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                        txtDocNO.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);

                        DataTable aTemp = aRegisterEditDocumentMN.GetNextAlphabet(Convert.ToString(aData.Rows[0]["Rev"]).Trim());
                        txtREV.Text =  aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["Word"]).Trim() : "Hết phiên bản" ;

                        txtDocName.Text = Convert.ToString(aData.Rows[0]["DocumentName"]);
                        //ddlDocType.Text = Convert.ToString(aData.Rows[0]["DocumentType"]);
                        //txtRevisionApplication.Text = Convert.ToString(aData.Rows[0]["RevisionApplication"]);
                        //txtCheckingNotice.Text = Convert.ToString(aData.Rows[0]["CheckingNotice"]);

                        txtDocumentObsolete.Text = Convert.ToString(aData.Rows[0]["DeletedDocumentOld"]);
                        txtDocumentReference.Text = Convert.ToString(aData.Rows[0]["ReferenceDocument"]);
                        //txtWordKey.Text = Convert.ToString(aData.Rows[0]["IndexWord"]);

                        hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                        //hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);
                        //hidDepartmentCheck.Value = Convert.ToString(aData.Rows[0]["DepartmentCheck"]);
                        //hidFileName.Value = Convert.ToString(aData.Rows[0]["ContentFile"]);
                        //hidNeedRelease.Value = Convert.ToString(aData.Rows[0]["NeedReleaseFile"]);

                        aListItem = ddlDepartment.Items.FindByValue(Convert.ToString(aData.Rows[0]["Department"]));
                        if (aListItem != null)
                        {
                            ddlDepartment.SelectedValue = Convert.ToString(aData.Rows[0]["Department"]);
                        }

                    }

                    #endregion
                }
                else if (!String.IsNullOrEmpty(EditDocument))
                {
                    #region
                    
                    DataTable aData = aRegisterEditDocumentMN.ListRegisterEditDocument(EditDocument, string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                    if (aData.Rows.Count > 0)
                    {

                        ddlDepartment.Items.Clear();
                        ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(Convert.ToString(aData.Rows[0]["CreatedBy"]));
                        ddlDepartment.DataValueField = "Department";
                        ddlDepartment.DataTextField = "NameDepartment";
                        ddlDepartment.DataBind();

                        txtApplicationName.Text = Convert.ToString(aData.Rows[0]["HoTen"]);
                        hidID.Value = Convert.ToString(aData.Rows[0]["ID"]);
                        hidEditDocument.Value = EditDocument;

                        hidPublishDocument.Value = Convert.ToString(aData.Rows[0]["PublishDocument"]);
                        hidStates.Value = Convert.ToString(aData.Rows[0]["States"]);
                        txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["EditDocument"]);
                        hidCodeDocument.Value = Convert.ToString(aData.Rows[0]["CodeDocument"]);

                        ddlApplicationSite.Text = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                        txtEffectiveDate.Text = Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).Year > 1900 ? Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).ToString("yyyy/MM/dd") : string.Empty;
                        txtDocNO.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);

                        txtREV.Text = Convert.ToString(aData.Rows[0]["Rev"]);
                        txtDocName.Text = Convert.ToString(aData.Rows[0]["DocumentName"]);
                        //ddlDocType.Text = Convert.ToString(aData.Rows[0]["DocumentType"]);
                        //txtRevisionApplication.Text = Convert.ToString(aData.Rows[0]["RevisionApplication"]);
                        //txtCheckingNotice.Text = Convert.ToString(aData.Rows[0]["CheckingNotice"]);

                        txtDocumentObsolete.Text = Convert.ToString(aData.Rows[0]["DeletedDocumentOld"]);
                        txtDocumentReference.Text = Convert.ToString(aData.Rows[0]["ReferenceDocument"]);
                        //txtWordKey.Text = Convert.ToString(aData.Rows[0]["IndexWord"]);

                        txtApplicationDate.Text = Convert.ToDateTime(aData.Rows[0]["ApplicationDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                        //hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);
                        //hidDepartmentCheck.Value = Convert.ToString(aData.Rows[0]["DepartmentCheck"]);
                        hidFileName.Value = Convert.ToString(aData.Rows[0]["ContentFile"]);
                        hidNeedRelease.Value = Convert.ToString(aData.Rows[0]["NeedReleaseFile"]);

                        aListItem = ddlDepartment.Items.FindByValue(Convert.ToString(aData.Rows[0]["Department"]));
                        if (aListItem != null)
                        {
                            ddlDepartment.SelectedValue = Convert.ToString(aData.Rows[0]["Department"]);
                        }

                    }

                    #endregion
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
                #endregion
            }
        }
    }
}