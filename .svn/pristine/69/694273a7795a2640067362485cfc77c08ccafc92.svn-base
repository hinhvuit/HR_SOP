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
    public partial class ListCheckWait : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00030"))
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

                CategorysMN aCategorysMN = new CategorysMN();
                string CatTypeCode = string.Empty;
                CatTypeCode = "CT-00010";
                cboStates.Items.Clear();
                cboStates.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, CatTypeCode);
                cboStates.DataValueField = "CatCode";
                cboStates.DataTextField = "CatName";
                cboStates.DataBind();
                cboStates.Items.Insert(0, new ListItem("---All---", "ALL"));
                
                txtFromApplicationDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
                txtToApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            }
        }
    }
}