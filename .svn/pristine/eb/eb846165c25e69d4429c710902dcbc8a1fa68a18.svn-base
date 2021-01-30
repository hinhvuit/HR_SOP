using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using HR_SOP.Models;
using System.Data;
using HR_SOP.Models.Manager;

namespace HR_SOP
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    lblUserName.Text = "帳號 : " + LoginSession.UserName();
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx");
                }
            }
        }

      
    }

}