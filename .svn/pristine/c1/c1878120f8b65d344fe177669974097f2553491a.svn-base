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
    public partial class TransferEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00031"))
                        {
                            Response.Redirect("NoPermitsion.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx?Url=" + Request.Url.PathAndQuery);
                }
                
                string PublishDocument = Request.QueryString["PublishDocument"];
                if (String.IsNullOrEmpty(PublishDocument))
                {
                    txtPublishDocument.Text = string.Empty;
                    txtPublishDocument.Enabled = true;
                }
                else
                {
                    txtPublishDocument.Text = PublishDocument;
                    txtPublishDocument.Enabled = false;
                }
                
                UsersMN aUsersMN = new UsersMN();
                ddlPerson.Items.Clear();
                ddlPerson.DataSource = aUsersMN.ListUsers(string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,"0");
                ddlPerson.DataValueField = "TenDangNhap";
                ddlPerson.DataTextField = "Search";
                ddlPerson.DataBind();

            }
        }
    }
}