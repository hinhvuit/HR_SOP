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
    public partial class ApplicationObsoletedSecurity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00016"))
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
                UsersMN aUsersMN = new UsersMN();
                CategorysMN aCategorysMN = new CategorysMN();
                ListItem aListItem = new ListItem();
                string CatTypeCode = string.Empty;
                CatTypeCode = "CT-00003"; //Application Site
                ddlApplicationSite.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlApplicationSite.DataValueField = "CatCode";
                ddlApplicationSite.DataTextField = "CatName";
                ddlApplicationSite.DataBind();

                //CatTypeCode = "CT-00004"; //Doc Type
                //ddlDocType.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                //ddlDocType.DataValueField = "CatCode";
                //ddlDocType.DataTextField = "CatName";
                //ddlDocType.DataBind();

                #endregion

                #region
                string PublishDocument = Convert.ToString(Request.QueryString["PublishDocument"]);
                string ObsoletedDocument = Convert.ToString(Request.QueryString["ObsoletedDocument"]);

                if (!String.IsNullOrEmpty(PublishDocument))
                {
                    #region
                    RegisterPublishSecurityMN aRegisterPublishDocumentMN = new RegisterPublishSecurityMN();
                    txtApplicationName.Text = LoginSession.FullName();
                    txtApplicationNO.Text = string.Empty;
                    txtApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    hidPublishDocument.Value = PublishDocument;

                    DataTable aData = aRegisterPublishDocumentMN.ListRegisterPublishDocument(PublishDocument, string.Empty, "C26", string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                    if (aData.Rows.Count > 0)
                    {
                        ddlApplicationSite.Text = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                        //hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                        txtDocNO.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);
                        txtREV.Text = Convert.ToString(aData.Rows[0]["Rev"]);
                        txtDocName.Text = Convert.ToString(aData.Rows[0]["DocumentName"]);

                        txtReleaseDate.Text = Convert.ToString(aData.Rows[0]["EffectiveDate_Text"]);
                        //ddlDocType.Text = Convert.ToString(aData.Rows[0]["DocumentType"]);
                        //hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);
                        //hidDepartmentCheck.Value = Convert.ToString(aData.Rows[0]["DepartmentCheck"]);

                        DataTable aTemp = new DataTable();
                        aTemp = aCategorysMN.ListCategorys(Convert.ToString(aData.Rows[0]["Department"]), string.Empty, string.Empty);
                        txtApplicationDep.Text = aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["CatName"]) : string.Empty;
                        txtDepartment.Text = aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["CatName"]) : string.Empty;
                        aTemp = aUsersMN.GetManagerCurrent();
                        txtManager.Text = aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["HoTen"]) : string.Empty;

                    }

                    #endregion
                }
                else
                {
                    if (!String.IsNullOrEmpty(ObsoletedDocument))
                    {
                        #region

                        ApplicationObsoletedSecurityMN aApplicationObsoletedDocumentMN = new ApplicationObsoletedSecurityMN();
                        DataTable aData = aApplicationObsoletedDocumentMN.ListApplicationObsoletedDocument(ObsoletedDocument, string.Empty, string.Empty,
                            string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                        if (aData.Rows.Count > 0)
                        {
                            txtApplicationName.Text = Convert.ToString(aData.Rows[0]["HoTen"]);
                            hidID.Value = Convert.ToString(aData.Rows[0]["ID"]);
                            hidObsoletedDocument.Value = ObsoletedDocument;
                            hidStates.Value = Convert.ToString(aData.Rows[0]["States"]);
                            txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["ObsoletedDocument"]);
                            hidPublishDocument.Value = Convert.ToString(aData.Rows[0]["PublishDocument"]);
                            txtEffectiveDate.Text = Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).Year > 1900 ? Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).ToString("yyyy/MM/dd") : string.Empty;
                            txtApplicationDate.Text = Convert.ToString(aData.Rows[0]["ApplicationDate_Text"]);

                            ddlApplicationSite.Text = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                            //hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                            txtDocNO.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);
                            txtREV.Text = Convert.ToString(aData.Rows[0]["Rev"]);
                            txtDocName.Text = Convert.ToString(aData.Rows[0]["DocumentName"]);

                            txtReleaseDate.Text = Convert.ToString(aData.Rows[0]["EffectiveDate_Text"]);
                            //ddlDocType.Text = Convert.ToString(aData.Rows[0]["DocumentType"]);
                            //hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);
                            //hidDepartmentCheck.Value = Convert.ToString(aData.Rows[0]["DepartmentCheck"]);

                            DataTable aTemp = new DataTable();
                            aTemp = aCategorysMN.ListCategorys(Convert.ToString(aData.Rows[0]["Department"]), string.Empty, string.Empty);
                            txtApplicationDep.Text = aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["CatName"]) : string.Empty;
                            txtDepartment.Text = aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["CatName"]) : string.Empty;
                            aTemp = aUsersMN.GetManagerCurrent();
                            txtManager.Text = aTemp.Rows.Count > 0 ? Convert.ToString(aTemp.Rows[0]["HoTen"]) : string.Empty;

                            txtReasonObsoleted.Text = Convert.ToString(aData.Rows[0]["ReasonObsoleted"]);

                        }

                        #endregion
                    }
                }
                #endregion
            }
        }
    }
}