﻿using HR_SOP.Models;
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
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00004"))
                        {
                            Response.Redirect("NoPermitsion.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx?Url=" + Request.Url.PathAndQuery);
                }
            }
        }
    }
}