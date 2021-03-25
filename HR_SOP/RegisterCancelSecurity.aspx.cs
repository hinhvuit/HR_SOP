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
    public partial class RegisterCancelSecurity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    #region
                    if (LoginSession.IsLogin())
                    {
                        if (!LoginSession.IsAdmin())
                        {
                            if (!LoginSession.IsView("MN-00011"))
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
                    #endregion

                    #region
                    string CodeDocument = Convert.ToString(Request.QueryString["CodeDocument"]);
                    string DocNo = Convert.ToString(Request.QueryString["DocNo"]);
                    string CancelDocument = Convert.ToString(Request.QueryString["CancelDocument"]);

                    CategorysMN aCategorysMN = new CategorysMN();
                    ListItem aListItem = new ListItem();
                    string CatTypeCode = string.Empty;
                    CatTypeCode = "CT-00003"; //Application Site
                    ddlApplicationSite.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                    ddlApplicationSite.DataValueField = "CatCode";
                    ddlApplicationSite.DataTextField = "CatName";
                    ddlApplicationSite.DataBind();

                    //CatTypeCode = "CT-00007"; //Type of closing application
                    //ddlCloseDocument.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                    //ddlCloseDocument.DataValueField = "CatCode";
                    //ddlCloseDocument.DataTextField = "CatName";
                    //ddlCloseDocument.DataBind();


                    UserInDepartmentMN aUserInDepartmentMN = new UserInDepartmentMN();
                    #endregion

                    #region
                    RegisterCancelSecurityMN aRegisterCancelDocumentMN = new RegisterCancelSecurityMN();
                    if (!String.IsNullOrEmpty(CodeDocument) && !String.IsNullOrEmpty(DocNo))
                    {
                        #region

                        ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(LoginSession.UserName());
                        ddlDepartment.DataValueField = "Department";
                        ddlDepartment.DataTextField = "NameDepartment";
                        ddlDepartment.DataBind();

                        DataTable aData = aRegisterCancelDocumentMN.ListDocmentRefByCodeDocumentAndCodeDCC(CodeDocument, DocNo);
                        txtApplicationName.Text = LoginSession.FullName();
                        txtApplicationNO.Text = string.Empty;
                        txtApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        if (aData.Rows.Count > 0)
                        {
                            aListItem = ddlApplicationSite.Items.FindByValue(Convert.ToString(aData.Rows[0]["ApplicationSite"]));
                            if (aListItem != null)
                            {
                                ddlApplicationSite.SelectedValue = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                            }
                            txtApplicationNo_DCC.Text = Convert.ToString(aData.Rows[0]["CodeDocument"]);
                            txtDocNo_DCC.Text = Convert.ToString(aData.Rows[0]["DocNo"]);
                            txtNameFile.Text = Convert.ToString(aData.Rows[0]["DocName"]);
                            //txtAssignedRevisor.Text = Convert.ToString(aData.Rows[0]["AssignedRevisor"]);
                            txtEstimatedCloseData.Text = Convert.ToString(aData.Rows[0]["EstimatedCloseDate"]);
                        }


                        #endregion
                    }
                    else if (!String.IsNullOrEmpty(CancelDocument))
                    {
                        #region

                        DataTable aData = aRegisterCancelDocumentMN.ListRegisterCancelDocument(CancelDocument, string.Empty, string.Empty, string.Empty,
                            string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                        if (aData.Rows.Count > 0)
                        {
                            ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(Convert.ToString(aData.Rows[0]["CreatedBy"]));
                            ddlDepartment.DataValueField = "Department";
                            ddlDepartment.DataTextField = "NameDepartment";
                            ddlDepartment.DataBind();


                            hidID.Value = Convert.ToString(aData.Rows[0]["ID"]);
                            hidCancelDocument.Value = Convert.ToString(aData.Rows[0]["CancelDocument"]);
                            hidStates.Value = Convert.ToString(aData.Rows[0]["States"]);

                            txtApplicationName.Text = Convert.ToString(aData.Rows[0]["HoTen"]);
                            txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["CancelDocument"]);
                            txtApplicationDate.Text = Convert.ToDateTime(aData.Rows[0]["ApplicationDate"]).ToString("yyyy/MM/dd HH:mm:ss");

                            aListItem = ddlApplicationSite.Items.FindByValue(Convert.ToString(aData.Rows[0]["ApplicationSite"]));
                            if (aListItem != null)
                            {
                                ddlApplicationSite.SelectedValue = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                            }

                            txtEffectiveDate.Text = Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).Year > 1900 ? Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).ToString("yyyy/MM/dd") : string.Empty;

                            //aListItem = ddlCloseDocument.Items.FindByValue(Convert.ToString(aData.Rows[0]["CloseDocument"]));
                            //if (aListItem != null)
                            //{
                            //    ddlCloseDocument.SelectedValue = Convert.ToString(aData.Rows[0]["CloseDocument"]);
                            //}

                            txtApplicationNo_DCC.Text = Convert.ToString(aData.Rows[0]["ApplicationNo_Code"]);
                            txtDocNo_DCC.Text = Convert.ToString(aData.Rows[0]["DocNo"]);
                            txtNameFile.Text = Convert.ToString(aData.Rows[0]["DocName"]);
                            //txtAssignedRevisor.Text = Convert.ToString(aData.Rows[0]["AssignedRevisor"]);
                            txtEstimatedCloseData.Text = Convert.ToString(aData.Rows[0]["EstimatedCloseDate"]);

                            txtReasonApplication.Text = Convert.ToString(aData.Rows[0]["ReasonOfApplication"]);
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