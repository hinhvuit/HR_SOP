using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using HR_SOP.Models;
using HR_SOP.Models.Manager;
using System.Data;

namespace HR_SOP.Account
{
    public partial class Login : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    string id_user = Request.Url.Query;
                    if (id_user.LastIndexOf("id_user") > 0)
                    {
                        id_user = id_user.Substring(id_user.LastIndexOf("id_user") + 8, (id_user.Length - id_user.LastIndexOf("id_user") - 8));
                        id_user = Utils.Decrypt(id_user);
                        if (String.IsNullOrEmpty(id_user))
                        {
                            HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                            if (aCookie != null)
                            {
                                Response.Redirect("/Default.aspx",false);
                            }
                        }
                        else
                        {
                            UsersMN aUsersMN = new UsersMN();
                            DataTable aData = aUsersMN.CheckLogin(string.Empty, string.Empty, id_user);
                            if (aData.Rows.Count > 0)
                            {
                                id_user = id_user.ToUpper();
                                LoginSession.Login(id_user);

                                string url = Request.Url.Query;
                                url = url.Substring(5, url.IndexOf("&id_user")-5);
                                Response.Redirect(url,false);
                            }
                            else
                            {


                                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                                if (aCookie != null)
                                {
                                    Response.Redirect("/Default.aspx",false);
                                }
                            }
                        }
                    }
                    else
                    {
                        HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                        if (aCookie != null)
                        {
                            Response.Redirect("/Default.aspx",false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                    if (aCookie != null)
                    {
                        Response.Redirect("/Default.aspx");
                    }
                }
            }
        }
    }
}