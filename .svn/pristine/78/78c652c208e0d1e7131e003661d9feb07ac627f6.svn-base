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
    public partial class CheckingNotice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00026") && !LoginSession.IsView("MN-00027"))
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
                CategorysMN aCategorysMN = new CategorysMN();
                ListItem aListItem = new ListItem();
                string CatTypeCode = string.Empty;
                CatTypeCode = "CT-00003"; //Application Site
                ddlApplicationSite.Items.Clear();
                ddlApplicationSite.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlApplicationSite.DataValueField = "CatCode";
                ddlApplicationSite.DataTextField = "CatName";
                ddlApplicationSite.DataBind();

                CatTypeCode = "CT-00004"; //Doc Type
                ddlDocType.Items.Clear();
                ddlDocType.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlDocType.DataValueField = "CatCode";
                ddlDocType.DataTextField = "CatName";
                ddlDocType.DataBind();
                
                CatTypeCode = "CT-00002"; //Department
                ddlCurrentDepartment.Items.Clear();
                ddlCurrentDepartment.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                ddlCurrentDepartment.DataValueField = "CatCode";
                ddlCurrentDepartment.DataTextField = "CatName";
                ddlCurrentDepartment.DataBind();
                ddlCurrentDepartment.Items.Insert(0, new ListItem("ALL", "ALL"));

                #endregion

                #region
                string DocumentNo = Convert.ToString(Request.QueryString["DocumentNo"]);
                string CodeCheck = Convert.ToString(Request.QueryString["CodeCheck"]);
                
                if (!String.IsNullOrEmpty(DocumentNo))
                {
                    #region
                    CheckingNoticeMN aCheckingNoticeMN = new CheckingNoticeMN();
                    UserInDepartmentMN aUserInDepartmentMN = new UserInDepartmentMN();
                    txtApplicationName.Text = LoginSession.FullName();

                    DataTable aTemp = aUserInDepartmentMN.ListDepartmentByUserName(LoginSession.UserName());
                    if (aTemp.Rows.Count > 0)
                    {
                        txtDepartment.Text = Convert.ToString(aTemp.Rows[0]["NameDepartment"]);
                    }
                    else
                    {
                        txtDepartment.Text = string.Empty;
                    }
                    txtApplicationNO.Text = string.Empty;
                    txtApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    DataTable aData = aCheckingNoticeMN.ListRegisterCheckingNotice(string.Empty, string.Empty, DocumentNo,
                    Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                    if (aData.Rows.Count > 0)
                    {
                        hidPublishDocument.Value = Convert.ToString(aData.Rows[0]["PublishDocument"]);

                        txtDocNO.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);
                        txtDocName.Text = Convert.ToString(aData.Rows[0]["DocumentName"]);
                        txtREV.Text = Convert.ToString(aData.Rows[0]["Rev"]);
                        ddlApplicationSite.SelectedValue = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                        ddlDocType.SelectedValue = Convert.ToString(aData.Rows[0]["DocumentType"]);
                        hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                        hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);

                        txtOriginalResponsible.Text = Convert.ToString(aData.Rows[0]["Original"]);
                        txtRemark.Text = Convert.ToString(aData.Rows[0]["Remark"]);
                    }

                    #endregion
                }
                else
                {
                    if (!String.IsNullOrEmpty(CodeCheck))
                    {
                        #region
                        CheckingNoticeMN aCheckingNoticeMN = new CheckingNoticeMN();
                        DataTable aData = aCheckingNoticeMN.ListCheckingNotice(CodeCheck,string.Empty,string.Empty,string.Empty, string.Empty,
                        Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                        if (aData.Rows.Count > 0)
                        {
                            txtDepartment.Text = Convert.ToString(aData.Rows[0]["Department_Name"]);
                            txtApplicationNO.Text = Convert.ToString(aData.Rows[0]["CodeCheck"]);
                            txtApplicationName.Text = Convert.ToString(aData.Rows[0]["HoTen"]);
                            txtApplicationDate.Text = Convert.ToDateTime(aData.Rows[0]["ApplicationDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                            hidPublishDocument.Value = Convert.ToString(aData.Rows[0]["PublishDocument"]);

                            txtDocNO.Text = Convert.ToString(aData.Rows[0]["DocumentNo"]);
                            txtDocName.Text = Convert.ToString(aData.Rows[0]["DocumentName"]);
                            txtREV.Text = Convert.ToString(aData.Rows[0]["Rev"]);
                            ddlApplicationSite.SelectedValue = Convert.ToString(aData.Rows[0]["ApplicationSite"]);
                            ddlDocType.SelectedValue = Convert.ToString(aData.Rows[0]["DocumentType"]);

                            txtOriginalResponsible.Text = Convert.ToString(aData.Rows[0]["Original"]);
                            ddlCurrentDepartment.SelectedValue = Convert.ToString(aData.Rows[0]["Department"]);
                            hidCurrentDirector.Value = Convert.ToString(aData.Rows[0]["Director"]);
                            txtCurrentDirector.Text = Convert.ToString(aData.Rows[0]["Director_Name"]);
                            txtRemark.Text = Convert.ToString(aData.Rows[0]["Remark"]);
                            
                            hidCodeCheck.Value = Convert.ToString(aData.Rows[0]["CodeCheck"]);
                            hidID.Value = Convert.ToString(aData.Rows[0]["ID"]);
                            hidStates.Value = Convert.ToString(aData.Rows[0]["States"]);
                            hidApplicableSite.Value = Convert.ToString(aData.Rows[0]["ApplicableSite"]);
                            hidApplicableBU.Value = Convert.ToString(aData.Rows[0]["ApplicableBU"]);
                            


                            if (hidStates.Value.Equals("F05"))
                            {
                                UsersMN aUsersMN = new UsersMN();
                                ddlPerson.Items.Clear();
                                ddlPerson.DataSource = aUsersMN.ListDetailUserByDepartment(Convert.ToString(aData.Rows[0]["Department"]), string.Empty);
                                ddlPerson.DataValueField = "TenDangNhap";
                                ddlPerson.DataTextField = "HoTen";
                                ddlPerson.DataBind();
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