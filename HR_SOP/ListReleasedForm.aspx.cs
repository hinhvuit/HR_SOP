﻿using HR_SOP.Models;
using HR_SOP.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR_SOP
{
    public partial class ListReleasedForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginSession.IsLogin())
                {
                    if (!LoginSession.IsAdmin())
                    {
                        if (!LoginSession.IsView("MN-00020"))
                        {
                            Response.Redirect("NoPermitsion.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx?Url=" + Request.Url.PathAndQuery);
                }

                CategorysMN aCategorysMN = new CategorysMN();
                ddlDepartment.Items.Clear();
                ddlDepartment.DataSource = aCategorysMN.ListCategorys(string.Empty, string.Empty, "CT-00002");
                ddlDepartment.DataValueField = "CatCode";
                ddlDepartment.DataTextField = "CatName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("---ALL---", "ALL"));
            }
        }
    }
}