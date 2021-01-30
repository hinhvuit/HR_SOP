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
    public partial class RegisterCodeDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (LoginSession.IsLogin())
                    {
                        if (!LoginSession.IsAdmin())
                        {
                            if (!LoginSession.IsView("MN-00001"))
                            {
                                Response.Redirect("NoPermitsion.aspx");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("/Account/Login.aspx?Url="+ Request.Url.PathAndQuery);
                    }
                    
                    hidUserName.Value = LoginSession.UserName();
                    
                    #region
                    string CodeDocument = Convert.ToString(Request.QueryString["CodeDocument"]);
                    CategorysMN aCategorysMN = new CategorysMN();
                    ListItem aListItem = new ListItem();
                    string CatTypeCode = string.Empty;
                    CatTypeCode = "CT-00003"; //Application Site
                    ddlApplicationSite.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                    ddlApplicationSite.DataValueField = "CatCode";
                    ddlApplicationSite.DataTextField = "CatName";
                    ddlApplicationSite.DataBind();
                    aListItem = ddlApplicationSite.Items.FindByValue("C-00005");
                    if (aListItem != null)
                    {
                        ddlApplicationSite.SelectedValue = "C-00005";
                    }

                    //CatTypeCode = "CT-00004"; //Doc Type
                    //ddlDocType.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                    //ddlDocType.DataValueField = "CatCode";
                    //ddlDocType.DataTextField = "CatName";
                    //ddlDocType.DataBind();
                    //aListItem = ddlDocType.Items.FindByValue("C-00006");
                    //if (aListItem != null)
                    //{
                    //    ddlDocType.SelectedValue = "C-00006";
                    //}


                    UserInDepartmentMN aUserInDepartmentMN = new UserInDepartmentMN();
                    #endregion

                    #region
                    if (String.IsNullOrEmpty(CodeDocument))
                    {
                        #region

                        ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(LoginSession.UserName());
                        ddlDepartment.DataValueField = "Department";
                        ddlDepartment.DataTextField = "NameDepartment";
                        ddlDepartment.DataBind();

                        txtApplicationName.Text = LoginSession.FullName();
                        txtApplicationNO.Text =  string.Empty;
                        txtApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        #endregion
                    }
                    else
                    {
                        #region
                        RegisterCodeDocumentMN aRegisterCodeDocumentMN = new RegisterCodeDocumentMN();
                        DataTable aData = aRegisterCodeDocumentMN.ListRegisterCodeDocument(CodeDocument, string.Empty, string.Empty,
                            string.Empty,string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                        if (aData.Rows.Count > 0)
                        {

                            ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(Convert.ToString(aData.Rows[0]["CreatedBy"]));
                            ddlDepartment.DataValueField = "Department";
                            ddlDepartment.DataTextField = "NameDepartment";
                            ddlDepartment.DataBind(); 


                            hidID.Value= Convert.ToString(aData.Rows[0]["ID"]);
                            hidCodeDocument.Value= Convert.ToString(aData.Rows[0]["CodeDocument"]);
                            hidStates.Value= Convert.ToString(aData.Rows[0]["States"]);
                            hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                            //hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);
                            
                            txtApplicationName.Text = Convert.ToString(aData.Rows[0]["HoTen"]);
                            txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["CodeDocument"]);
                            txtApplicationDate.Text = Convert.ToDateTime(aData.Rows[0]["ApplicationDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                            ddlApplicationSite.SelectedValue= Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                            txtEffectiveDate.Text = Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).Year > 1900 ? Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).ToString("yyyy/MM/dd") :string.Empty ;
                            //ddlDocType.SelectedValue = Convert.ToString(aData.Rows[0]["DocumentType"]);
                            txtReasonApplication.Text = Convert.ToString(aData.Rows[0]["ReasonApplication"]);
                            
                            aListItem = ddlDepartment.Items.FindByValue(Convert.ToString(aData.Rows[0]["Department"]));
                            if (aListItem != null)
                            {
                                ddlDepartment.SelectedValue = Convert.ToString(aData.Rows[0]["Department"]);
                            }
                        }

                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}