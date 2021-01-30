using HR_SOP.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HR_SOP.Models
{
    public static class LoginSession
    {
        public static void Login(string UserName)
        {
            try
            {
                string Quyen = string.Empty;
                string Xem = string.Empty;
                string ThemMoi = string.Empty;
                string Sua = string.Empty;

                string Xoa = string.Empty;
                string BaoCao = string.Empty;
                string TimKiem = string.Empty;
                string ResetPass = string.Empty;

                string HoTen = string.Empty;
                string ViTri = string.Empty;
                
                UsersMN aUsersMN = new UsersMN();
                DataTable aData = new DataTable();
                aData = aUsersMN.ListPermitsionByUserName(UserName);

                #region
                if (aData.Rows.Count > 0)
                {
                    Quyen = Convert.ToString(aData.Rows[0]["Quyen"]);
                    HoTen = Convert.ToString(aData.Rows[0]["HoTen"]);
                    ViTri = Convert.ToBoolean(aData.Rows[0]["DCC"]) ? "1" : string.Empty;
                }
                else
                {
                    Quyen = string.Empty;
                    HoTen = string.Empty;
                    ViTri = string.Empty;
                }

                UserInPositionMN aUserInPositionMN = new UserInPositionMN();
                DataTable aPosition  = aUserInPositionMN.CheckPosition(UserName);
                if (aPosition.Rows.Count > 0)
                {
                    ViTri = "1";
                }

                #endregion

                #region
                for (int i = 0; i < aData.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(aData.Rows[i]["Xem"]))
                    {
                        Xem = Xem + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }

                    if (Convert.ToBoolean(aData.Rows[i]["ThemMoi"]))
                    {
                        ThemMoi = ThemMoi + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }

                    if (Convert.ToBoolean(aData.Rows[i]["Sua"]))
                    {
                        Sua = Sua + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }

                    if (Convert.ToBoolean(aData.Rows[i]["Xoa"]))
                    {
                        Xoa = Xoa + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }

                    if (Convert.ToBoolean(aData.Rows[i]["BaoCao"]))
                    {
                        BaoCao = BaoCao + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }

                    if (Convert.ToBoolean(aData.Rows[i]["TimKiem"]))
                    {
                        TimKiem = TimKiem + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }

                    if (Convert.ToBoolean(aData.Rows[i]["ResetPass"]))
                    {
                        ResetPass = ResetPass + Convert.ToString(aData.Rows[i]["Code"]) + "|";
                    }
                }
                #endregion

                #region
                HttpCookie aCookie = new HttpCookie("Login");
                
                aCookie.Values["UserName"] = UserName;
                aCookie.Values["Quyen"] = Quyen;
                aCookie.Values["Xem"] = Xem;
                aCookie.Values["ThemMoi"] = ThemMoi;
                aCookie.Values["Sua"] = Sua;

                aCookie.Values["Xoa"] = Xoa;
                aCookie.Values["BaoCao"] = BaoCao;
                aCookie.Values["TimKiem"] = TimKiem;
                aCookie.Values["ResetPass"] = ResetPass;
                aCookie.Values["FullName"] = HttpContext.Current.Server.UrlEncode(HoTen);

                aCookie.Values["ViTri"] = ViTri;

                aCookie.Expires.AddHours(8);
                HttpContext.Current.Response.Cookies.Add(aCookie);
                #endregion
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static void Logout()
        {
            HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
            if (aCookie != null)
            {
                HttpContext.Current.Response.Cookies["Login"].Expires = DateTime.Now.AddHours(-8);
            }

            HttpCookie aCookieMenu = HttpContext.Current.Request.Cookies["MENU"];
            if (aCookieMenu != null)
            {
                HttpContext.Current.Response.Cookies["MENU"].Expires = DateTime.Now.AddHours(-4);
            }
        }

        public static bool IsAdmin()
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["Quyen"]).Equals("Admin"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsLogin()
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static string ViTri()
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    return HttpContext.Current.Server.UrlDecode(aCookie.Values["ViTri"]);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        public static string UserName()
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    return Convert.ToString(aCookie.Values["UserName"]);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        public static string FullName()
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    return HttpContext.Current.Server.UrlDecode(aCookie.Values["FullName"]);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }
        
        public static bool IsView(string  Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["Xem"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsAdd(string Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["ThemMoi"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsEdit(string Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["Sua"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsDelete(string Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["Xoa"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsReport(string Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["BaoCao"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsSearch(string Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["TimKiem"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool IsResetPass(string Code)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies["Login"];
                if (aCookie != null)
                {
                    if (Convert.ToString(aCookie.Values["ResetPass"]).Contains(Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
    }
}