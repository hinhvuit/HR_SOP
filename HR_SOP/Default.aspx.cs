using HR_SOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_SOP
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!LoginSession.IsLogin())
                {
                    Response.Redirect("/Account/Login.aspx");
                }
            }
           
        }
    }
}