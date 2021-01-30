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
    public partial class RenewalsDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00028"))
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
                RenewalsDocumentMN aRenewalsDocumentMN = new RenewalsDocumentMN();
                CategorysMN aCategorysMN = new CategorysMN();
                ListItem aListItem = new ListItem();
                string CatTypeCode = string.Empty;
                CatTypeCode = "CT-00003"; //Application Site
                ddlApplicationSite.Items.Clear();
                ddlApplicationSite.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlApplicationSite.DataValueField = "CatCode";
                ddlApplicationSite.DataTextField = "CatName";
                ddlApplicationSite.DataBind();
                #endregion

                #region
                string Code = Convert.ToString(Request.QueryString["Code"]);
                string DCC = Convert.ToString(Request.QueryString["DCC"]);
                string RenewalCode = Convert.ToString(Request.QueryString["RenewalCode"]);

                if (!String.IsNullOrEmpty(Code) && !String.IsNullOrEmpty(DCC))
                {
                    #region
                    ddlDepartment.Items.Clear();
                    ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(LoginSession.UserName());
                    ddlDepartment.DataValueField = "Department";
                    ddlDepartment.DataTextField = "NameDepartment";
                    ddlDepartment.DataBind();

                    hidType.Value = Convert.ToString(Request.QueryString["Type"]);
                    txtApplicationName.Text = LoginSession.FullName();
                    txtApplicationNO.Text = string.Empty;
                    txtApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    DataTable aData = aRenewalsDocumentMN.ListRegisterRenewalDocument(Code, DCC, string.Empty);
                    if (aData.Rows.Count > 0)
                    {
                        aListItem = ddlApplicationSite.Items.FindByValue(Convert.ToString(aData.Rows[0]["ApplicationSite"]));
                        if (aListItem != null)
                        {
                            ddlApplicationSite.SelectedValue = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                        }
                        aListItem = ddlType.Items.FindByValue(Convert.ToString(aData.Rows[0]["Type"]));
                        if (aListItem != null)
                        {
                            ddlType.SelectedValue = Convert.ToString(aData.Rows[0]["Type"]);
                        }
                        txtApplicationCode.Text= Convert.ToString(aData.Rows[0]["Code"]);
                        txtDocNO.Text = Convert.ToString(aData.Rows[0]["DCC"]);
                        txtRevised.Text = Convert.ToString(aData.Rows[0]["Revised"]);
                        txtRevisor.Text = Convert.ToString(aData.Rows[0]["Revisor"]);
                        txtCloseDate.Text = Convert.ToString(aData.Rows[0]["CloseDate"]);
                    }

                    #endregion
                }
                else
                {
                    if (!String.IsNullOrEmpty(RenewalCode))
                    {
                        #region
                        ddlDepartment.Items.Clear();
                        ddlDepartment.DataSource = aUserInDepartmentMN.ListDepartmentByUserName(LoginSession.UserName());
                        ddlDepartment.DataValueField = "Department";
                        ddlDepartment.DataTextField = "NameDepartment";
                        ddlDepartment.DataBind();

                        DataTable aData = aRenewalsDocumentMN.ListRenewalsDocument(RenewalCode, string.Empty, string.Empty, string.Empty, string.Empty,
                        Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                        if (aData.Rows.Count > 0)
                        {
                            hidID.Value = Convert.ToString(aData.Rows[0]["ID"]);
                            hidStates.Value = Convert.ToString(aData.Rows[0]["States"]);
                            hidRenewalCode.Value = Convert.ToString(aData.Rows[0]["RenewalCode"]);

                            txtApplicationName.Text = Convert.ToString(aData.Rows[0]["HoTen"]);
                            txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["RenewalCode"]);
                            txtApplicationDate.Text = Convert.ToDateTime(aData.Rows[0]["ApplicationDate"]).ToString("yyyy/MM/dd HH:mm:ss");

                            aListItem = ddlApplicationSite.Items.FindByValue(Convert.ToString(aData.Rows[0]["ApplicationSite"]));
                            if (aListItem != null)
                            {
                                ddlApplicationSite.SelectedValue = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                            }
                            aListItem = ddlType.Items.FindByValue(Convert.ToString(aData.Rows[0]["TypeRenewal"]));
                            if (aListItem != null)
                            {
                                ddlType.SelectedValue = Convert.ToString(aData.Rows[0]["TypeRenewal"]);
                            }
                            aListItem = ddlDepartment.Items.FindByValue(Convert.ToString(aData.Rows[0]["Department"]));
                            if (aListItem != null)
                            {
                                ddlDepartment.SelectedValue = Convert.ToString(aData.Rows[0]["Department"]);
                            }

                            txtApplicationCode.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);
                            txtDocNO.Text = Convert.ToString(aData.Rows[0]["DCC_NO"]);
                            txtRevised.Text = Convert.ToString(aData.Rows[0]["BeforRevised"]);
                            txtRevisor.Text = Convert.ToString(aData.Rows[0]["BeforRevisor"]);
                            txtCloseDate.Text = Convert.ToString(aData.Rows[0]["BeforCloseDate"]);

                            txtEffectiveDate.Text = Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).Year > 1900 ? Convert.ToDateTime(aData.Rows[0]["EffectiveDate"]).ToString("yyyy/MM/dd") : string.Empty;
                            txtReason.Text = Convert.ToString(aData.Rows[0]["Reason"]);
                            txtAfterRevisor.Text = Convert.ToString(aData.Rows[0]["Revisor"]);
                            txtAfterCloseDate.Text = Convert.ToDateTime(aData.Rows[0]["CloseDate"]).Year > 1900 ? Convert.ToDateTime(aData.Rows[0]["CloseDate"]).ToString("yyyy/MM/dd") : string.Empty;


                            if (hidStates.Value.Equals("H05") || hidStates.Value.Equals("H10"))
                            {
                                UsersMN aUsersMN = new UsersMN();
                                ddlDirecter.Items.Clear();
                                ddlDirecter.DataSource = aUsersMN.ListDetailUserByDepartment(string.Empty, "C-00004");
                                ddlDirecter.DataValueField = "TenDangNhap";
                                ddlDirecter.DataTextField = "HoTen";
                                ddlDirecter.DataBind();
                                ddlDirecter.Items.Insert(0, new ListItem("ALL", "ALL"));
                                if (hidStates.Value.Equals("H10"))
                                {
                                    ddlDirecter.SelectedIndex = 1;
                                }
                            }

                        }
                        #endregion
                    }
                }
                #endregion
            }
        }
    }
}