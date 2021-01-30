using HR_SOP.Models;
using HR_SOP.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_SOP
{
    public partial class ListEditDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00022"))
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
                //hidCheckWaitEdit.Value = LoginSession.ViTri();

                CategorysMN aCategorysMN = new CategorysMN();
                //string CatTypeCode = string.Empty;
                //CatTypeCode = "States_Edit";
                //cboStates.Items.Clear();
                //cboStates.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                //cboStates.DataValueField = "CatCode";
                //cboStates.DataTextField = "CatName";
                //cboStates.DataBind();
                //cboStates.Items.Insert(0, new ListItem("---All---", "ALL"));

                ddlDepartment.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, "CT-00002");
                ddlDepartment.DataValueField = "CatCode";
                ddlDepartment.DataTextField = "CatName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "ALL"));

                txtFromApplicationDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
                txtToApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            }
        }
    }
}