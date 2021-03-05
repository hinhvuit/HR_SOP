using HR_SOP.Models.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HR_SOP.Models
{
    /// <summary>
    /// Summary description for WebServiceDB
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServiceDB : System.Web.Services.WebService
    {

        #region Roles

        RolesMN aRolesMN = new RolesMN();

        [WebMethod]
        public string ListRoles(string Code, string Name, string IsDeleted)
        {
            return JsonConvert.SerializeObject(aRolesMN.ListRoles(Code, Name, IsDeleted));
        }

        [WebMethod]
        public string InsertOrUpdateRole(string ID, string Code, string Name, string IsDeleted)
        {
            string aInfo = string.Empty;
            int aNum = aRolesMN.InsertOrUpdateRole(ID, Code, Name, IsDeleted, LoginSession.UserName());
            if (aNum > 0)
            {
                if (String.IsNullOrEmpty(ID))
                {
                    aInfo = "ADD";
                }
                else
                {
                    aInfo = "EDIT";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedRole(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aRolesMN.DeletedRole(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        #endregion

        #region Users

        UsersMN aUsersMN = new UsersMN();

        [WebMethod]
        public string ListUsers(string TenDangNhap, string HoTen, string SoDienThoai, string Email,
            string ChucVu, string PhongBan, string IsDeleted)
        {
            ChucVu = ChucVu.Equals("null") ? string.Empty : ChucVu;
            PhongBan = PhongBan.Equals("null") ? string.Empty : PhongBan;
            return JsonConvert.SerializeObject(aUsersMN.ListUsers(TenDangNhap, HoTen, SoDienThoai, Email, ChucVu, PhongBan, IsDeleted));
        }

        [WebMethod]
        public string ListUserManagerRoom(string Username)
        {
            return JsonConvert.SerializeObject(aUsersMN.ListUserManagerRoom(Username));
        }

        [WebMethod]
        public string ListDetailUserByDepartment(string Department, string Position)
        {
            return JsonConvert.SerializeObject(aUsersMN.ListDetailUserByDepartment(Department, Position));
        }

        [WebMethod]
        public string InsertOrUpdateUser(string ID, string TenDangNhap, string MatKhau,
            string HoTen, string ChucVu, string PhongBan, string IsDeleted, string Email, string SoDienThoai, string DCC, string Manager_Room)
        {
            UserInDepartmentMN aUserInDepartmentMN = new UserInDepartmentMN();
            UserInPositionMN aUserInPositionMN = new UserInPositionMN();

            ChucVu = ChucVu.Equals("null") ? string.Empty : ChucVu;
            PhongBan = PhongBan.Equals("null") ? string.Empty : PhongBan;
            string aInfo = string.Empty;
            int aNum = aUsersMN.InsertOrUpdateUser(ID, TenDangNhap, MatKhau, HoTen, LoginSession.UserName(), IsDeleted, Email, SoDienThoai, DCC);
            if (aNum == 1)
            {
                aInfo = "ADD";
                aNum = aUserInDepartmentMN.InsertUserInDepartment(TenDangNhap, PhongBan, LoginSession.UserName());
                aNum = aUserInPositionMN.InsertUserInPosition(TenDangNhap, ChucVu, LoginSession.UserName());
                aNum = aUsersMN.DeleteManager_Room(TenDangNhap);
                string[] UserManager = Manager_Room.Split(',');
                for (int i = 0; i < UserManager.Length; i++)
                {
                    if (!String.IsNullOrEmpty(UserManager[i]) && !UserManager[i].Equals("null"))
                    {
                        aNum = aUsersMN.InsertManager_Room(TenDangNhap, UserManager[i]);
                    }
                }

                if (aNum < 1)
                {
                    aInfo = "ERROR";
                }

            }
            else if (aNum == 2)
            {
                aInfo = "EDIT";
                aNum = aUserInDepartmentMN.InsertUserInDepartment(TenDangNhap, PhongBan, LoginSession.UserName());
                aNum = aUserInPositionMN.InsertUserInPosition(TenDangNhap, ChucVu, LoginSession.UserName());
                aNum = aUsersMN.DeleteManager_Room(TenDangNhap);
                string[] UserManager = Manager_Room.Split(',');
                for (int i = 0; i < UserManager.Length; i++)
                {
                    if (!String.IsNullOrEmpty(UserManager[i]) && !UserManager[i].Equals("null"))
                    {
                        aNum = aUsersMN.InsertManager_Room(TenDangNhap, UserManager[i]);
                    }
                }
                if (aNum < 1)
                {
                    aInfo = "ERROR";
                }
            }
            else if (aNum == 3)
            {
                aInfo = "EXIST";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedUser(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aUsersMN.DeletedUser(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string ResetPassword(string ID, string MatKhau)
        {
            string aInfo = string.Empty;
            int aNum = aUsersMN.ResetPassword(ID, MatKhau);
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string ChangePassword(string MatKhau)
        {
            string aInfo = string.Empty;
            int aNum = aUsersMN.ChangePassword(LoginSession.UserName(), MatKhau);
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        #endregion

        #region Categorys
        CategorysMN aCategorysMN = new CategorysMN();
        [WebMethod]
        public string ListCategorys(string CatCode, string CatName, string CatTypeCode)
        {
            return JsonConvert.SerializeObject(aCategorysMN.ListCategorys(CatCode, CatName, CatTypeCode));
        }

        [WebMethod]
        public string GetCategorysByCatTypeCode(string CatTypeCode)
        {
            return JsonConvert.SerializeObject(aCategorysMN.GetCategorysByCatTypeCode(CatTypeCode));
        }

        [WebMethod]
        public string InsertOrUpdateCategorys(string ID, string CatCode, string CatName, string CatTypeCode, string Orders)
        {
            string aInfo = string.Empty;
            int aNum = aCategorysMN.InsertOrUpdateCategorys(ID, CatCode, CatName, CatTypeCode, Orders, LoginSession.UserName());
            if (aNum > 0)
            {
                if (String.IsNullOrEmpty(ID))
                {
                    aInfo = "ADD";
                }
                else
                {
                    aInfo = "EDIT";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedCategorys(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aCategorysMN.DeletedCategorys(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        #endregion

        #region CategoryType
        CategoryTypeMN aCategoryTypeMN = new CategoryTypeMN();
        [WebMethod]
        public string ListCategoryType()
        {
            return JsonConvert.SerializeObject(aCategoryTypeMN.ListCategoryType());
        }

        [WebMethod]
        public string InsertOrUpdateCategoryType(string ID, string CatTypeCode, string CatTypeName, string Orders)
        {
            string aInfo = string.Empty;
            int aNum = aCategoryTypeMN.InsertOrUpdateCategoryType(ID, CatTypeCode, CatTypeName, Orders, LoginSession.UserName());
            if (aNum > 0)
            {
                if (String.IsNullOrEmpty(ID))
                {
                    aInfo = "ADD";
                }
                else
                {
                    aInfo = "EDIT";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedCategoryType(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aCategoryTypeMN.DeletedCategoryType(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }
        #endregion

        #region UsersInRoles

        UsersInRolesMN aUsersInRolesMN = new UsersInRolesMN();

        [WebMethod]
        public string ListUsersInRoles(string UserName, string CodeRole)
        {
            return JsonConvert.SerializeObject(aUsersInRolesMN.ListUsersInRoles(UserName, CodeRole));
        }

        [WebMethod]
        public string ListRolesCheck(string UserName)
        {
            return JsonConvert.SerializeObject(aUsersInRolesMN.ListRolesCheck(UserName));
        }

        [WebMethod]
        public string InsertUsersInRoles(string UserName, string CodeRole)
        {
            CodeRole = String.IsNullOrEmpty(CodeRole) ? string.Empty : CodeRole;
            string aInfo = string.Empty;
            int aNum = aUsersInRolesMN.InsertUsersInRoles(UserName, CodeRole, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }

            return JsonConvert.SerializeObject(aInfo);
        }
        #endregion

        #region Modules
        ModulesMN aModulesMN = new ModulesMN();

        [WebMethod]
        public string ListSetPermitsion(string CodeRole)
        {
            return JsonConvert.SerializeObject(aModulesMN.ListSetPermitsion(CodeRole));
        }

        [WebMethod]
        public string InsertOrUpdateModule(string CodeRole, string Content)
        {
            #region
            int index = 0;
            string aInfo = string.Empty;
            string ID = string.Empty; string Code = string.Empty; string Xem = string.Empty; string ThemMoi = string.Empty;
            string Sua = string.Empty; string Xoa = string.Empty; string BaoCao = string.Empty; string TimKiem = string.Empty; string ResetPass = string.Empty;
            Content = "<table>" + Content + "</table>";
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(Content);
            System.Xml.XmlNodeList nodeList_tr = xd.GetElementsByTagName("tr");
            foreach (System.Xml.XmlNode node_tr in nodeList_tr)
            {
                index = 0;
                System.Xml.XmlNodeList nodeList_td = node_tr.SelectNodes("td");
                foreach (System.Xml.XmlNode notde_td in nodeList_td)
                {
                    if (index == 0)
                    {
                        index++;
                    }
                    else if (index == 1)
                    {
                        Code = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 2)
                    {
                        index++;
                    }
                    else if (index == 3)
                    {
                        Xem = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 4)
                    {
                        ThemMoi = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 5)
                    {
                        Sua = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 6)
                    {
                        Xoa = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 7)
                    {
                        BaoCao = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 8)
                    {
                        TimKiem = notde_td.InnerText;
                        index++;
                    }
                    else if (index == 9)
                    {
                        ResetPass = notde_td.InnerText;
                        index++;
                    }
                    else
                    {
                        ID = notde_td.InnerText;
                        index++;
                    }
                }
                int aNum = 0;
                aNum = aModulesMN.InsertOrUpdateModule(ID, CodeRole, Code, Xem, ThemMoi,
                Sua, Xoa, BaoCao, TimKiem, ResetPass, LoginSession.UserName());
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                    break;
                }
            }
            return JsonConvert.SerializeObject(aInfo);
            #endregion

        }
        #endregion

        #region gobal
        [WebMethod]
        public string GetCodeAuto(string NameCol, string NameTable)
        {
            return JsonConvert.SerializeObject(Utils.GetCodeAuto(NameCol, NameTable));
        }

        [WebMethod]
        public string SaveFileCodeDoc()
        {
            try
            {
                string fileName = string.Empty;
                string filePath = string.Empty;
                fileName = HttpContext.Current.Request.Files["FileName"].FileName;
                fileName = fileName.Replace("+", "");
                string[] sl = fileName.Split('\\');
                if (sl.Length > 1)
                {
                    fileName = sl[sl.Length - 1];
                }
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString().Substring(0, 8) + "_"
                + Guid.NewGuid().ToString().Substring(0, 8) + "_" + fileName;

                filePath = Server.MapPath("~/Updatafile/CodeDoc/" + fileName);
                HttpContext.Current.Request.Files["FileName"].SaveAs(filePath);
                return fileName;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        [WebMethod]
        public string DeleteFileCodeDoc(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/CodeDoc/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return JsonConvert.SerializeObject(FileName);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("Error");
            }
        }
        #endregion

        #region Login
        [WebMethod]
        public string CheckLogin(string Username, string Password)
        {
            Username = Username.ToUpper();
            string aInfo = string.Empty;
            DataTable aData = new DataTable();
            aData = aUsersMN.CheckLogin(Username, Password, string.Empty);
            if (aData.Rows.Count > 0)
            {
                LoginSession.Login(Username);
                aInfo = "SUCCESS";
            }
            else
            {
                /*if (aUsersMN.CheckData(Username))
                {
                    aUsersMN.GetDataHR(Username);
                }*/
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }
        #endregion

        #region Menus
        MenusMN aMenusMN = new MenusMN();
        [WebMethod]
        public string GetMenus()
        {
            return JsonConvert.SerializeObject(aMenusMN.GetMenus(LoginSession.UserName()));
        }

        [WebMethod]
        public string ListMenus(string Code, string Name)
        {
            return JsonConvert.SerializeObject(aMenusMN.ListMenus(Code, Name));
        }

        [WebMethod]
        public string InsertOrUpdateMenus(string ID, string Code, string Name, string Url, string Orders, string Groups)
        {
            string aInfo = string.Empty;
            int aNum = aMenusMN.InsertOrUpdateMenus(ID, Code, Name, Url, LoginSession.UserName(), Orders, Groups);
            if (aNum > 0)
            {
                if (String.IsNullOrEmpty(ID))
                {
                    aInfo = "ADD";
                }
                else
                {
                    aInfo = "EDIT";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedMenus(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aMenusMN.DeletedMenus(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }


        #endregion

        #region RegisterCodeDocument
        RegisterCodeDocumentMN aRegisterCodeDocumentMN = new RegisterCodeDocumentMN();

        [WebMethod]
        public string ListRegisterCodeDocument(string CodeDocument, string CreatedBy, string States, string CheckWait, string Type,
           string Department, string DocNo, string DocName, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListRegisterCodeDocument(CodeDocument, CreatedBy, States, CheckWait, Type,
               Department, DocNo, DocName, FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string InsertOrUpdateRegisterCodeDocument(string ID, string CodeDocument, string ApplicationSite, string sEffectiveDate, string DocumentType,
            string ReasonApplication, string ApplicableSite, string ApplicableBU, string sApplicationDate, string Ref, string Department)
        {
            DateTime EffectiveDate = String.IsNullOrEmpty(sEffectiveDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sEffectiveDate);
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);
            if (String.IsNullOrEmpty(ID))
            {

                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                CodeDocument = "SOP-A-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("CodeDocument", "RegisterCodeDocument");
            }

            string aInfo = "ERROR";
            int aNum = aRegisterCodeDocumentMN.InsertOrUpdateRegisterCodeDocument(ID, CodeDocument, LoginSession.UserName(), ApplicationSite, EffectiveDate,
            DocumentType, ReasonApplication, ApplicableSite, ApplicableBU, ApplicationDate, "A01", string.Empty, Department);
            if (aNum > 0)
            {
                aNum = aRegisterCodeDocumentMN.InsertApprovalSection(CodeDocument, LoginSession.UserName(), Department);
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    DataTable aTemp = aRegisterCodeDocumentMN.ListDocumentRef(CodeDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        DeleteFileCodeDoc(Convert.ToString(aTemp.Rows[i]["FileName"]).Trim());
                    }
                    aNum = aRegisterCodeDocumentMN.DeleteDocumentRef(CodeDocument);
                    if (aNum > 0)
                    {
                        aInfo = "SUCCESS";
                        #region
                        string DocumentName = string.Empty;
                        string FileName = string.Empty;
                        string AssignedRevisor = string.Empty;
                        DateTime EstimatedCloseDate = Convert.ToDateTime("1900/01/01");
                        int index = 0;
                        int OrderBy = 0;

                        Ref = "<table>" + Ref + "</table>";
                        System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                        xd.LoadXml(Ref);
                        System.Xml.XmlNodeList nodeList_tr = xd.GetElementsByTagName("tr");
                        foreach (System.Xml.XmlNode node_tr in nodeList_tr)
                        {
                            OrderBy++;
                            index = 0;
                            System.Xml.XmlNodeList nodeList_td = node_tr.SelectNodes("td");
                            foreach (System.Xml.XmlNode notde_td in nodeList_td)
                            {
                                if (index == 0)
                                {
                                    index++;
                                }
                                else if (index == 1)
                                {
                                    DocumentName = notde_td.InnerText;
                                    index++;
                                }
                                else if (index == 2)
                                {
                                    FileName = notde_td.InnerText;
                                    index++;
                                }
                                //else if (index == 3)
                                //{
                                //    AssignedRevisor = notde_td.InnerText;
                                //    index++;
                                //}
                                else if (index == 3)
                                {
                                    EstimatedCloseDate = String.IsNullOrEmpty(notde_td.InnerText.Trim()) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(notde_td.InnerText.Trim());
                                    index++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            aRegisterCodeDocumentMN.InsertDocumentRef(DocumentName, FileName, AssignedRevisor, EstimatedCloseDate, LoginSession.UserName(), CodeDocument, OrderBy);
                        }
                        #endregion
                    }
                    else
                    {
                        aInfo = "ERROR";
                    }
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }

            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = CodeDocument;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedRegisterCodeDocument(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aRegisterCodeDocumentMN.DeletedRegisterCodeDocument(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string AcceptRegisterCodeDocument(string CodeDocument, string States, string Comment, string Url, string Ref)
        {
            #region
            UsersMN aUsersMN = new UsersMN();
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("A01"))
            {
                DataTable aCheck = aUsersMN.CheckLevel(LoginSession.UserName());
                if (aCheck.Rows.Count > 0)
                {
                    string Position = aCheck.Rows[0]["Position"].ToString().Trim();
                    if (Position.Equals("TT")){
                        States = "A05";
                    }
                    else if (Position.Equals("TP"))
                    {
                        States = "A10";
                    }
                    else {
                        States = "A03";
                    }
                }
                else {
                    States = "A03";
                }
                
            }
            else if (States.Equals("A03"))
            {
                int TS = aRegisterCodeDocumentMN.CheckApprovalSection_Code(CodeDocument, States);
                if (TS > 1)
                {
                    States = "A03";
                }
                else
                {
                    States = "A05";
                }
            }
            else if (States.Equals("A05"))
            {
                int TS = aRegisterCodeDocumentMN.CheckApprovalSection_Code(CodeDocument, States);
                if (TS > 1)
                {
                    States = "A05";
                }
                else
                {
                    States = "A10";
                }
            }
            else if (States.Equals("A10"))
            {
                States = "A15";
            }
            #endregion

            aNum = aRegisterCodeDocumentMN.AcceptRegisterCodeDocument(CodeDocument, LoginSession.UserName(), States);
            if (aNum > 0)
            {
                #region
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(CodeDocument, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region Gui email

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;

                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    
                    DataTable aTemp = aRegisterCodeDocumentMN.CheckListSendMail_Code(CodeDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("A15"))
                            {
                                Utils.ContentSendMail_Accept(CodeDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else
                            {
                                Utils.ContentSendMail_WaitSign(CodeDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }

                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);

                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(CodeDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
                #endregion

                #region
                if (States.Equals("A15"))
                {
                    #region
                    string DocNo = string.Empty; string DocName = string.Empty; int OrderBy = 0; string ID_DocumentRef = string.Empty;
                    int index = 0;

                    Ref = "<table>" + Ref + "</table>";
                    System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                    xd.LoadXml(Ref);
                    System.Xml.XmlNodeList nodeList_tr = xd.GetElementsByTagName("tr");
                    foreach (System.Xml.XmlNode node_tr in nodeList_tr)
                    {
                        OrderBy++;
                        index = 0;
                        System.Xml.XmlNodeList nodeList_td = node_tr.SelectNodes("td");
                        foreach (System.Xml.XmlNode notde_td in nodeList_td)
                        {
                            if (index == 0)
                            {
                                ID_DocumentRef = notde_td.Attributes["value"].Value;
                                index++;
                            }
                            else if (index == 1)
                            {

                                DocNo = notde_td.InnerText;
                                index++;
                            }
                            else if (index == 2)
                            {
                                DocName = notde_td.InnerText;
                                index++;
                            }
                            else
                            {

                                continue;
                            }
                        }
                        aRegisterCodeDocumentMN.InsertDCC_Ref(DocNo, DocName, LoginSession.UserName(), CodeDocument, OrderBy, ID_DocumentRef);
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string RejectRegisterCodeDocument(string CodeDocument, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = A20 : Lam lai don
            aNum = aRegisterCodeDocumentMN.RejectRegisterCodeDocument(CodeDocument, LoginSession.UserName(), "A20");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(CodeDocument, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aRegisterCodeDocumentMN.CheckListSendMail_Code(CodeDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(CodeDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(CodeDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string DeleteDocumentRef(string CodeDocument)
        {
            string aInfo = string.Empty;
            int aNum = aRegisterCodeDocumentMN.DeleteDocumentRef(CodeDocument);
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string ListDocumentRef(string CodeDocument)
        {
            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListDocumentRef(CodeDocument));
        }

        [WebMethod]
        public string ListApprovalSection(string CodeDocument)
        {
            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListApprovalSection(CodeDocument));
        }

        [WebMethod]
        public string InsertApprovalSection(string CodeDocument, string UserName, string Department)
        {
            string aInfo = string.Empty;
            int aNum = aRegisterCodeDocumentMN.InsertApprovalSection(CodeDocument, UserName, Department);
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string CheckDisplaySubmitCodeDocument(string CodeDocument)
        {
            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.CheckDisplaySubmitCodeDocument(CodeDocument));
        }

        [WebMethod]
        public string ListDCC_Ref(string CodeDocument, string Status)
        {
            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListDCC_Ref(CodeDocument, Status));
        }

        [WebMethod]
        public string CheckExistDocNo(string DocNo)
        {
            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.CheckExistDocNo(DocNo));
        }

        [WebMethod]
        public string ListRegisterCodeDocumentByDCC(string CodeDocument, string DocNo, string StatusDCC,
            string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListRegisterCodeDocumentByDCC(CodeDocument, LoginSession.UserName(), DocNo, StatusDCC,
                FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string ListSearchDocument(string DocName, string CreatedBy, string DocNo, string StatusDCC,
            string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListSearchDocument(DocName, CreatedBy, DocNo, StatusDCC,
                FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string ListCheckWait(string Code, string Dcc, string Department, string CheckWait, string CreatedBy,
            string sFromDate, string sToDate, string Type)
        {
            DateTime FromDate = String.IsNullOrEmpty(sFromDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sFromDate);
            DateTime ToDate = String.IsNullOrEmpty(sToDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sToDate);

            return JsonConvert.SerializeObject(aRegisterCodeDocumentMN.ListCheckWait(Code, Dcc, Department, CheckWait, CreatedBy, FromDate, ToDate, Type));
        }

        [WebMethod]
        public string CheckExistsFileCodeDocument(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/CodeDoc/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    return JsonConvert.SerializeObject("EXISTED");
                }
                return JsonConvert.SerializeObject("NOT_EXISTED");
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("ERROR");
            }
        }

        #endregion

        #region RegisterPublishDocument
        RegisterPublishDocumentMN aRegisterPublishDocumentMN = new RegisterPublishDocumentMN();

        [WebMethod]
        public string ListRegisterPublishDocument(string PublishDocument, string CreatedBy, string States, string CheckWait,
            string DocumentNo, string DocumentName, string IndexWord,string Department, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(tApplicationDate);
            return JsonConvert.SerializeObject(aRegisterPublishDocumentMN.ListRegisterPublishDocument(PublishDocument, CreatedBy, States, CheckWait,
                DocumentNo, DocumentName, IndexWord, Department, FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string ListRegisterPublishDocumentByType(string PublishDocument, string Type, string DCC, string fApplicationDate, string tApplicationDate)
        {
            PublishDocument = PublishDocument.Trim();
            DCC = DCC.Trim();
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);
            return JsonConvert.SerializeObject(aRegisterPublishDocumentMN.ListRegisterPublishDocumentByType(PublishDocument, LoginSession.UserName(), Type, DCC, FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string InsertOrUpdateRegisterPublishDocument(string ID, string PublishDocument, string ApplicationSite, string sEffectiveDate, string DocumentNo,
            string Rev, string DocumentName, string DocumentType, string RevisionApplication, string CheckingNotice,
            string DeletedDocumentOld, string ReferenceDocument, string IndexWord, string ContentFile, string ContentFile_old, string PublishReff,
            string ApplicableSite, string ApplicableBU, string NeedReleaseFile, string NeedReleaseFile_old, string sApplicationDate,
            string DepartmentCheck, string CodeDocument, string Department)
        {


            DateTime EffectiveDate = String.IsNullOrEmpty(sEffectiveDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sEffectiveDate);
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);

            if (String.IsNullOrEmpty(ID))
            {

                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                PublishDocument = "SOP-C-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("PublishDocument", "RegisterPublishDocument");
            }
            else
            {
                if (!String.IsNullOrEmpty(ContentFile))
                {
                    if (!String.IsNullOrEmpty(ContentFile_old))
                    {
                        DeleteFilePublish(ContentFile_old);
                    }
                }
                ContentFile = String.IsNullOrEmpty(ContentFile) ? ContentFile_old : ContentFile;

                if (!String.IsNullOrEmpty(NeedReleaseFile))
                {
                    if (!String.IsNullOrEmpty(NeedReleaseFile_old))
                    {
                        DeleteFilePublishNeed(NeedReleaseFile_old);
                    }
                }
                NeedReleaseFile = String.IsNullOrEmpty(NeedReleaseFile) ? NeedReleaseFile_old : NeedReleaseFile;
            }

            string aInfo = "ERROR";
            int aNum = aRegisterPublishDocumentMN.InsertOrUpdateRegisterPublishDocument(ID, PublishDocument, LoginSession.UserName(), ApplicationSite, EffectiveDate, DocumentNo,
             Rev, DocumentName, DocumentType, RevisionApplication, CheckingNotice, DeletedDocumentOld, ReferenceDocument, IndexWord,
             ContentFile, string.Empty, ApplicableSite, ApplicableBU, NeedReleaseFile, string.Empty, "C01", string.Empty, ApplicationDate, DepartmentCheck, CodeDocument, Department);
            if (aNum > 0)
            {

                //string from = System.IO.Path.Combine(Server.MapPath("~/Updatafile/CodeDoc/"), ContentFile);
                //string to = System.IO.Path.Combine(Server.MapPath("~/Updatafile/PublishDoc/"), ContentFile);
                //if (System.IO.File.Exists(to))
                //{
                //    try
                //    {
                //        System.IO.File.Delete(to);
                //    }
                //    catch (System.IO.IOException ex)
                //    {
                //        ex.ToString();
                //    }
                //}

                //if (System.IO.File.Exists(from))
                //{ 

                //    try
                //    {
                //        System.IO.File.Copy(from, to, true);
                //    }
                //    catch (Exception ex)
                //    {
                //        ex.ToString();
                //    }
                //}


                    
                
                aNum = aRegisterPublishDocumentMN.InsertApprovalSection_PublishDocument(PublishDocument, LoginSession.UserName(), DepartmentCheck, Department);
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                }

                #region
                PublishReffMN aPublishReffMN = new PublishReffMN();
                DataTable aTemp = aPublishReffMN.ListPublishReff(PublishDocument, string.Empty, string.Empty, string.Empty, string.Empty,
                Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                for (int i = 0; i < aTemp.Rows.Count; i++)
                {
                    DeleteFilePublishRefferent(Convert.ToString(aTemp.Rows[i]["Attachment"]).Trim());
                }

                aNum = aPublishReffMN.DeletedPublishReff(PublishDocument);
                if (aNum > 0)
                {
                    #region
                    string FormNo = string.Empty;
                    string FormName = string.Empty;
                    string PreservingDepartment = string.Empty;
                    DateTime PreservingTime = Convert.ToDateTime("1900/01/01");
                    string FilePublishReff = string.Empty;
                    int index = 0;
                    int OrderBy = 0;
                    PublishReff = "<table>" + PublishReff + "</table>";
                    System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                    xd.LoadXml(PublishReff);
                    System.Xml.XmlNodeList nodeList_tr = xd.GetElementsByTagName("tr");
                    foreach (System.Xml.XmlNode node_tr in nodeList_tr)
                    {
                        OrderBy++;
                        index = 0;
                        System.Xml.XmlNodeList nodeList_td = node_tr.SelectNodes("td");
                        foreach (System.Xml.XmlNode notde_td in nodeList_td)
                        {
                            if (index == 0)
                            {
                                index++;
                            }
                            else if (index == 1)
                            {
                                FormNo = notde_td.InnerText;
                                index++;
                            }
                            else if (index == 2)
                            {
                                FormName = notde_td.InnerText;
                                index++;
                            }
                            else if (index == 3)
                            {
                                PreservingDepartment = notde_td.Attributes["value"].Value;
                                index++;
                            }
                            else if (index == 4)
                            {
                                PreservingTime = String.IsNullOrEmpty(notde_td.InnerText.Trim()) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(notde_td.InnerText.Trim());
                                index++;
                            }
                            else if (index == 5)
                            {
                                FilePublishReff = notde_td.InnerText;
                                index++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        aPublishReffMN.InsertPublishReff(PublishDocument, FormNo, FormName, PreservingDepartment, PreservingTime, FilePublishReff, "1", LoginSession.UserName(), OrderBy);
                    }
                    #endregion
                }
                #endregion

            }
            else
            {
                aInfo = "ERROR";
            }
            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = PublishDocument;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeleteFilePublish(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return JsonConvert.SerializeObject(FileName);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("Error");
            }
        }

        [WebMethod]
        public string DeleteFilePublishNeed(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/NeedRelease/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return JsonConvert.SerializeObject(FileName);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("Error");
            }
        }

        [WebMethod]
        public string CheckDisplaySubmitPublishDocument(string PublishDocument)
        {
            return JsonConvert.SerializeObject(aRegisterPublishDocumentMN.CheckDisplaySubmitPublishDocument(PublishDocument, LoginSession.UserName()));
        }

        [WebMethod]
        public string CreateCodeFormAuto(string Code_Dcc, string NameCol)
        {
            return JsonConvert.SerializeObject(aRegisterPublishDocumentMN.CreateCodeFormAuto(Code_Dcc, NameCol, "Categorys"));
        }

        [WebMethod]
        public string AcceptRegisterPublishDocument(string PublishDocument, string States, string Comment, string Url,string DepartmentCheck)
        {
            #region
            UsersMN aUsersMN = new UsersMN();
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("C01"))
            {
                States = "C25";
                //DataTable aCheck = aUsersMN.CheckLevel(LoginSession.UserName());
                //if (aCheck.Rows.Count > 0)
                //{
                //    string Position = aCheck.Rows[0]["Position"].ToString().Trim();
                //    if (Position.Equals("TT"))
                //    {
                //        States = "C05";
                //    }
                //    else if (Position.Equals("TP"))
                //    {
                //        States = "C10";
                //    }
                //    else
                //    {
                //        States = "C03";
                //    }
                //}
                //else
                //{
                //    States = "C03";
                //}
            }
            //else if (States.Equals("C03"))
            //{
            //    int TS = aRegisterPublishDocumentMN.CheckApprovalSection(PublishDocument, States);
            //    if (TS > 1)
            //    {
            //        States = "C03";
            //    }
            //    else
            //    {
            //        States = "C05";
            //    }
            //}
            //else if (States.Equals("C05"))
            //{
            //    int TS = aRegisterPublishDocumentMN.CheckApprovalSection(PublishDocument, States);
            //    if (TS > 1)
            //    {
            //        States = "C05";
            //    }
            //    else
            //    {
            //        States = "C10";
            //    }
            //}
            //else if (States.Equals("C10"))
            //{
            //    if (String.IsNullOrEmpty(DepartmentCheck))
            //    {
            //        States = "C20";
            //    }
            //    else
            //    {
            //        States = "C15";                   
            //    }
                
            //}
            //else if (States.Equals("C15"))
            //{
            //    int TS = aRegisterPublishDocumentMN.CheckApprovalSection(PublishDocument,States);
            //    if (TS > 1)
            //    {
            //        States = "C15";
            //    }
            //    else
            //    {
            //        States = "C20";
            //    }
            //}
            //else if (States.Equals("C20"))
            //{
            //    States = "C25";
            //}
            else if (States.Equals("C25"))
            {
                States = "C26";
            }
            #endregion

            #region
            aNum = aRegisterPublishDocumentMN.AcceptRegisterPublishDocument(PublishDocument, LoginSession.UserName(), States);
            if (aNum > 0)
            {
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(PublishDocument, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region
                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;



                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    
                    DataTable aTemp = aRegisterPublishDocumentMN.CheckListSendMail_Publish(PublishDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("C26"))
                            {
                                Utils.ContentSendMail_Accept(PublishDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else
                            {
                                Utils.ContentSendMail_WaitSign(PublishDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(PublishDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());
                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
            #endregion
        }

        [WebMethod]
        public string RejectRegisterPublishDocument(string PublishDocument, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = C30 : Lam lai don
            aNum = aRegisterPublishDocumentMN.RejectRegisterPublishDocument(PublishDocument, LoginSession.UserName(), "C30");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(PublishDocument, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aRegisterPublishDocumentMN.CheckListSendMail_Publish(PublishDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(PublishDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(PublishDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());
                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string DeletedRegisterPublishDocument(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aRegisterPublishDocumentMN.DeletedRegisterPublishDocument(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string SaveFilePublishDoc()
        {
            try
            {
                string fileName = string.Empty;
                string filePath = string.Empty;
                fileName = HttpContext.Current.Request.Files["FileName"].FileName;
                fileName = fileName.Replace("+", "");
                string[] sl = fileName.Split('\\');
                if (sl.Length > 1)
                {
                    fileName = sl[sl.Length - 1];
                }
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString().Substring(0, 8) + "_"
                + Guid.NewGuid().ToString().Substring(0, 8) + "_" + fileName;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/" + fileName);
                HttpContext.Current.Request.Files["FileName"].SaveAs(filePath);
                return fileName;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        [WebMethod]
        public string SaveFilePublishNeedRelease()
        {
            try
            {
                string Version = HttpContext.Current.Request.Params["Version"].ToString();
                string fileName = string.Empty;
                string filePath = string.Empty;
                fileName = HttpContext.Current.Request.Files["FileNameNeedRelease"].FileName;
                fileName = fileName.Replace("+", "");
                string[] sl = fileName.Split('\\');
                if (sl.Length > 1)
                {
                    fileName = sl[sl.Length - 1];
                }
                fileName = Version + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString().Substring(0, 8) + "_"
                + Guid.NewGuid().ToString().Substring(0, 8) + "_" + fileName;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/NeedRelease/" + fileName);
                HttpContext.Current.Request.Files["FileNameNeedRelease"].SaveAs(filePath);
                return fileName;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        [WebMethod]
        public string SearchRegisterPublishDocument(string DocumentNo, string ListDocumentNo)
        {
            return JsonConvert.SerializeObject(aRegisterPublishDocumentMN.SearchRegisterPublishDocument(DocumentNo, ListDocumentNo));
        }

        [WebMethod]
        public string CheckExistsFilePublishReff(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/Refferent/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    return JsonConvert.SerializeObject("EXISTED");
                }
                return JsonConvert.SerializeObject("NOT_EXISTED");
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("ERROR");
            }
        }

        [WebMethod]
        public string CheckExistsFilePublishDocument(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    return JsonConvert.SerializeObject("EXISTED");
                }
                return JsonConvert.SerializeObject("NOT_EXISTED");
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("ERROR");
            }
        }

        [WebMethod]
        public string CheckExistsFilePublishDocumentNeed(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/NeedRelease/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    return JsonConvert.SerializeObject("EXISTED");
                }
                return JsonConvert.SerializeObject("NOT_EXISTED");
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("ERROR");
            }
        }

        [WebMethod]
        public string UpdateRegisterPublishDocumentByPublishDocument(string PublishDocument, string UserEdit, string StatusEdit, string Url)
        {
            #region
            string aInfo = string.Empty;
            int aNum = 0;
            aNum = aRegisterPublishDocumentMN.UpdateRegisterPublishDocumentByPublishDocument(PublishDocument, UserEdit, StatusEdit);
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
                if (StatusEdit.Equals("2"))
                {
                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;
                    string UserName = string.Empty;
                    string FullName = string.Empty;
                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aData = aUsersMN.ListUsers(UserEdit, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                    if (aData.Rows.Count > 0)
                    {
                        Url = Url + "/RegisterEditDocument.aspx?PublishDocument=" + PublishDocument;
                        ToEmail = Convert.ToString(aData.Rows[0]["Email"]);
                        UserName = UserEdit;
                        FullName = Convert.ToString(aData.Rows[0]["HoTen"]);
                        Utils.ContentSendMail_Edit(PublishDocument, Url, FullName, UserName, ref Title, ref Contents);
                        bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                        Status = bS ? "SUCCESS" : "ERRER";
                        ToEmail = ToEmail + "___" + UserName;
                        aHistorySendMailMN.InsertHistorySendMail(PublishDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());
                    }
                }

            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
            #endregion
        }

        #endregion

        #region UserInDepartment

        UserInDepartmentMN aUserInDepartmentMN = new UserInDepartmentMN();

        [WebMethod]
        public string ListDepartmentInUser(string UserName)
        {
            return JsonConvert.SerializeObject(aUserInDepartmentMN.ListDepartmentInUser(UserName));
        }

        [WebMethod]
        public string InsertUserInDepartment(string UserName, string Department)
        {
            Department = String.IsNullOrEmpty(Department) ? string.Empty : Department;
            string aInfo = string.Empty;
            int aNum = aUserInDepartmentMN.InsertUserInDepartment(UserName, Department, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }

            return JsonConvert.SerializeObject(aInfo);
        }
        #endregion

        #region UserInPosition
        UserInPositionMN aUserInPositionMN = new UserInPositionMN();
        [WebMethod]
        public string ListPositionInUser(string UserName)
        {
            return JsonConvert.SerializeObject(aUserInPositionMN.ListPositionInUser(UserName));
        }

        [WebMethod]
        public string InsertUserInPosition(string UserName, string Position)
        {
            Position = String.IsNullOrEmpty(Position) ? string.Empty : Position;
            string aInfo = string.Empty;
            int aNum = aUserInPositionMN.InsertUserInPosition(UserName, Position, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }

            return JsonConvert.SerializeObject(aInfo);
        }
        #endregion

        #region RegisterCancelDocument
        RegisterCancelDocumentMN aRegisterCancelDocumentMN = new RegisterCancelDocumentMN();

        [WebMethod]
        public string ListRegisterCancelDocument(string CancelDocument, string DocNo_DCC,string DocName, string CreatedBy,
            string States, string CheckWait, string Type,string Department, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aRegisterCancelDocumentMN.ListRegisterCancelDocument(CancelDocument, DocNo_DCC, DocName, CreatedBy, States,
                CheckWait, Type, Department, FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string InsertOrUpdateRegisterCancelDocument(string ID, string CancelDocument, string sApplicationDate, string sEffectiveDate, string ApplicationSite,
            string CloseDocument, string ApplicationNo_Code, string DocNo_DCC, string ReasonOfApplication, string Department)
        {
            DateTime EffectiveDate = String.IsNullOrEmpty(sEffectiveDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sEffectiveDate);
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);
            if (String.IsNullOrEmpty(ID))
            {

                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                CancelDocument = "SOP-G-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("CancelDocument", "RegisterCancelDocument");
            }

            string aInfo = "ERROR";
            int aNum = aRegisterCancelDocumentMN.InsertOrUpdateRegisterCancelDocument(ID, CancelDocument, ApplicationDate, EffectiveDate, ApplicationSite, CloseDocument, ApplicationNo_Code,
                DocNo_DCC, ReasonOfApplication, "G01", LoginSession.UserName(), Department);
            if (aNum > 0)
            {
                aNum = aRegisterCancelDocumentMN.InsertApprovalSection_CancelDocument(CancelDocument, LoginSession.UserName(), Department);
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }

            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = CancelDocument;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedRegisterCancelDocument(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aRegisterCancelDocumentMN.DeletedRegisterCancelDocument(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string AcceptRegisterCancelDocument(string CancelDocument, string States, string Comment, string Url)
        {
            #region
            UsersMN aUsersMN = new UsersMN();
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("G01"))
            {
                DataTable aCheck = aUsersMN.CheckLevel(LoginSession.UserName());
                if (aCheck.Rows.Count > 0)
                {
                    string Position = aCheck.Rows[0]["Position"].ToString().Trim();
                    if (Position.Equals("TT"))
                    {
                        States = "G05";
                    }
                    else if (Position.Equals("TP"))
                    {
                        States = "G10";
                    }
                    else
                    {
                        States = "G03";
                    }
                }
                else
                {
                    States = "G03";
                }
            }
            else if (States.Equals("G03"))
            {
                int TS = aRegisterCancelDocumentMN.CheckApprovalSection_Cancel(CancelDocument, States);
                if (TS > 1)
                {
                    States = "G03";
                }
                else
                {
                    States = "G05";
                }
            }
            else if (States.Equals("G05"))
            {
                int TS = aRegisterCancelDocumentMN.CheckApprovalSection_Cancel(CancelDocument, States);
                if (TS > 1)
                {
                    States = "G05";
                }
                else
                {
                    States = "G10";
                }
            }
            else if (States.Equals("G10"))
            {
                States = "G15";
            }
            #endregion

            aNum = aRegisterCancelDocumentMN.AcceptRegisterCancelDocument(CancelDocument, LoginSession.UserName(), States);
            if (aNum > 0)
            {
                #region
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(CancelDocument, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region Gui email

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;



                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    
                    DataTable aTemp = aRegisterCancelDocumentMN.CheckListSendMail_Cancel(CancelDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("G15"))
                            {
                                Utils.ContentSendMail_Accept(CancelDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else
                            {
                                Utils.ContentSendMail_WaitSign(CancelDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(CancelDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
                #endregion
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string RejectRegisterCancelDocument(string CancelDocument, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = 20 : Lam lai don
            aNum = aRegisterCancelDocumentMN.RejectRegisterCancelDocument(CancelDocument, LoginSession.UserName(), "G20");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(CancelDocument, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aRegisterCodeDocumentMN.CheckListSendMail_Code(CancelDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(CancelDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(CancelDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());
                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string CheckDisplaySubmitCancelDocument(string CancelDocument)
        {
            return JsonConvert.SerializeObject(aRegisterCancelDocumentMN.sp_CheckDisplaySubmitCancelDocument(CancelDocument));
        }

        #endregion

        #region Application Obsoleted Document
        ApplicationObsoletedDocumentMN aApplicationObsoletedDocumentMN = new ApplicationObsoletedDocumentMN();

        [WebMethod]
        public string ListApplicationObsoletedDocument(string ObsoletedDocument, string CreatedBy, string States, string CheckWait,
            string DocNo, string DocName, string Department, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(tApplicationDate);
            return JsonConvert.SerializeObject(aApplicationObsoletedDocumentMN.ListApplicationObsoletedDocument(ObsoletedDocument, CreatedBy, States, CheckWait, 
               DocNo,DocName,Department,FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string InsertOrUpdateApplicationObsoletedDocument(string ID, string ObsoletedDocument, string PublishDocument, string ApplicationSite,
            string sEffectiveDate, string ReasonObsoleted, string sApplicationDate)
        {
            DateTime EffectiveDate = String.IsNullOrEmpty(sEffectiveDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sEffectiveDate);
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);

            if (String.IsNullOrEmpty(ID))
            {

                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                ObsoletedDocument = "SOP-D-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("ObsoletedDocument", "ApplicationObsoletedDocument");
            }

            string aInfo = "ERROR";
            int aNum = aApplicationObsoletedDocumentMN.InsertOrUpdateApplicationObsoletedDocument(ID, ObsoletedDocument, PublishDocument,
                EffectiveDate, ReasonObsoleted, "D01", ApplicationDate, LoginSession.UserName());
            if (aNum > 0)
            {
                aNum = aApplicationObsoletedDocumentMN.InsertApprovalSection_ObsoletedDocument(ObsoletedDocument, LoginSession.UserName());
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = ObsoletedDocument;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string CheckDisplaySubmitObsoletedDocument(string ObsoletedDocument)
        {
            return JsonConvert.SerializeObject(aApplicationObsoletedDocumentMN.CheckDisplaySubmitObsoletedDocument(ObsoletedDocument, LoginSession.UserName()));
        }

        [WebMethod]
        public string AcceptAcceptApplicationObsoletedDocument(string ObsoletedDocument, string States, string Comment, string Url)
        {
            #region
            UsersMN aUsersMN = new UsersMN();
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("D01"))
            {
                DataTable aCheck = aUsersMN.CheckLevel(LoginSession.UserName());
                if (aCheck.Rows.Count > 0)
                {
                    string Position = aCheck.Rows[0]["Position"].ToString().Trim();
                    if (Position.Equals("TT"))
                    {
                        States = "D05";
                    }
                    else if (Position.Equals("TP"))
                    {
                        States = "D10";
                    }
                    else
                    {
                        States = "D03";
                    }
                }
                else
                {
                    States = "D03";
                }
            }
            else if (States.Equals("D03"))
            {
                int TS = aApplicationObsoletedDocumentMN.CheckApprovalSection_ObsoletedDocument(ObsoletedDocument, States);
                if (TS > 1)
                {
                    States = "D03";
                }
                else
                {
                    States = "D05";
                }
            }
            else if (States.Equals("D05"))
            {
                int TS = aApplicationObsoletedDocumentMN.CheckApprovalSection_ObsoletedDocument(ObsoletedDocument, States);
                if (TS > 1)
                {
                    States = "D05";
                }
                else
                {
                    //States = "D10";
                    States = "D25";
                }
            }
            //else if (States.Equals("D10"))
            //{
            //    States = "D15";
            //}
            //else if (States.Equals("D15"))
            //{
            //    int TS = aApplicationObsoletedDocumentMN.CheckApprovalSection_ObsoletedDocument(ObsoletedDocument,States);
            //    if (TS > 1)
            //    {
            //        States = "D15";
            //    }
            //    else
            //    {
            //        States = "D20";
            //    }

            //}
            //else if (States.Equals("D20"))
            //{
            //    States = "D25";
            //}
            else if (States.Equals("D25"))
            {
                States = "D26";
            }
            #endregion

            #region
            aNum = aApplicationObsoletedDocumentMN.AcceptAcceptApplicationObsoletedDocument(ObsoletedDocument, LoginSession.UserName(), States);
            if (aNum > 0)
            {
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(ObsoletedDocument, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;



                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    
                    DataTable aTemp = aApplicationObsoletedDocumentMN.CheckListSendMail_Obsoleted(ObsoletedDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("D26"))
                            {
                                Utils.ContentSendMail_Accept(ObsoletedDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else
                            {
                                Utils.ContentSendMail_WaitSign(ObsoletedDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(ObsoletedDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
            #endregion
        }

        [WebMethod]
        public string RejectApplicationObsoletedDocument(string ObsoletedDocument, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = D30 : Lam lai don
            aNum = aApplicationObsoletedDocumentMN.RejectApplicationObsoletedDocument(ObsoletedDocument, LoginSession.UserName(), "D30");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(ObsoletedDocument, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aApplicationObsoletedDocumentMN.CheckListSendMail_Obsoleted(ObsoletedDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(ObsoletedDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(ObsoletedDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string DeletedApplicationObsoletedDocument(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aApplicationObsoletedDocumentMN.DeletedApplicationObsoletedDocument(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        #endregion

        #region PublishReff
        PublishReffMN aPublishReffMN = new PublishReffMN();
        [WebMethod]
        public string ListPublishReff(string PublishDocument, string FormNo, string FormName, string PreservingDepartment, string sFromDate, string sToDate)
        {
            DateTime FromDate = String.IsNullOrEmpty(sFromDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sFromDate);
            DateTime ToDate = String.IsNullOrEmpty(sToDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sToDate);

            return JsonConvert.SerializeObject(aPublishReffMN.ListPublishReff(PublishDocument, FormNo, FormName, PreservingDepartment, "C26", FromDate, ToDate));
        }

        [WebMethod]
        public string ListPublishReffByPublishDocument(string PublishDocument)
        {
            return JsonConvert.SerializeObject(aPublishReffMN.ListPublishReff(PublishDocument, string.Empty, string.Empty, string.Empty, string.Empty,
                Convert.ToDateTime("1900/01/01"), Convert.ToDateTime("1900/01/01")));
        }

        [WebMethod]
        public string ListPublishReffByEditDocument(string EditDocument)
        {
            return JsonConvert.SerializeObject(aPublishReffMN.ListPublishReffByEditDocument(EditDocument));
        }

        [WebMethod]
        public string SaveFilePublishRefferent()
        {
            try
            {
                string fileName = string.Empty;
                string filePath = string.Empty;
                fileName = HttpContext.Current.Request.Files["filePublishAttach"].FileName;
                fileName = fileName.Replace("+", "");
                string[] sl = fileName.Split('\\');
                if (sl.Length > 1)
                {
                    fileName = sl[sl.Length - 1];
                }
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString().Substring(0, 8) + "_"
                + Guid.NewGuid().ToString().Substring(0, 8) + "_" + fileName;

                filePath = Server.MapPath("~/Updatafile/PublishDoc/Refferent/" + fileName);
                HttpContext.Current.Request.Files["filePublishAttach"].SaveAs(filePath);
                return fileName;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        [WebMethod]
        public string DeleteFilePublishRefferent(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/Refferent/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return JsonConvert.SerializeObject(FileName);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("Error");
            }
        }

        #endregion

        #region RegisterEditDocument
        RegisterEditDocumentMN aRegisterEditDocumentMN = new RegisterEditDocumentMN();

        [WebMethod]
        public string ListRegisterEditDocument(string EditDocument, string CreatedBy, string States, string CheckWait,
            string DocumentNo, string DocumentName, string IndexWord,string Department, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1901/01/01") : Convert.ToDateTime(tApplicationDate);
            return JsonConvert.SerializeObject(aRegisterEditDocumentMN.ListRegisterEditDocument(EditDocument, CreatedBy, States, CheckWait,
                DocumentNo, DocumentName, IndexWord, Department, FromApplicationDate, ToApplicationDate));
        }


        [WebMethod]
        public string InsertOrUpdateRegisterEditDocument(string ID, string EditDocument, string PublishDocument, string ApplicationSite, string sEffectiveDate, string DocumentNo,
            string Rev, string DocumentName, string DocumentType, string RevisionApplication, string CheckingNotice,
            string DeletedDocumentOld, string ReferenceDocument, string IndexWord, string ContentFile, string ContentFile_old, string PublishReff,
            string ApplicableSite, string ApplicableBU, string NeedReleaseFile, string NeedReleaseFile_old, string sApplicationDate,
            string DepartmentCheck, string CodeDocument, string Department, string Status)
        {
            DateTime EffectiveDate = String.IsNullOrEmpty(sEffectiveDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sEffectiveDate);
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);
            Status = String.IsNullOrEmpty(Status) ? "EDIT" : "CHECK";

            if (String.IsNullOrEmpty(ID))
            {

                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                EditDocument = "SOP-B-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("EditDocument", "RegisterEditDocument");
            }
            else
            {
                if (!String.IsNullOrEmpty(ContentFile))
                {
                    DeleteFileEdit(ContentFile_old);
                }
                ContentFile = String.IsNullOrEmpty(ContentFile) ? ContentFile_old : ContentFile;

                if (!String.IsNullOrEmpty(NeedReleaseFile))
                {
                    if (!String.IsNullOrEmpty(NeedReleaseFile_old))
                    {
                        DeleteFilePublishNeed(NeedReleaseFile_old);
                    }
                }
                NeedReleaseFile = String.IsNullOrEmpty(NeedReleaseFile) ? NeedReleaseFile_old : NeedReleaseFile;
            }

            string aInfo = "ERROR";
            int aNum = aRegisterEditDocumentMN.InsertOrUpdateRegisterEditDocument(ID, EditDocument, PublishDocument, LoginSession.UserName(), ApplicationSite, EffectiveDate, DocumentNo,
             Rev, DocumentName, DocumentType, RevisionApplication, CheckingNotice, DeletedDocumentOld, ReferenceDocument, IndexWord,
             ContentFile, string.Empty, ApplicableSite, ApplicableBU, NeedReleaseFile, Status, "B01", string.Empty, ApplicationDate, DepartmentCheck, CodeDocument, Department);
            if (aNum > 0)
            {
                aNum = aRegisterEditDocumentMN.InsertApprovalSection_EditDocument(EditDocument, LoginSession.UserName(), DepartmentCheck, Department);
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                }

                #region
                PublishReffMN aPublishReffMN = new PublishReffMN();
                DataTable aTemp = aPublishReffMN.ListPublishReff(EditDocument, string.Empty, string.Empty, string.Empty, string.Empty,
                Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));
                for (int i = 0; i < aTemp.Rows.Count; i++)
                {
                    DeleteFilePublishRefferent(Convert.ToString(aTemp.Rows[i]["Attachment"]).Trim());
                }

                aNum = aPublishReffMN.DeletedPublishReff(EditDocument);
                if (aNum > 0)
                {
                    #region
                    string FormNo = string.Empty;
                    string FormName = string.Empty;
                    string PreservingDepartment = string.Empty;
                    DateTime PreservingTime = Convert.ToDateTime("1900/01/01");
                    string FilePublishReff = string.Empty;
                    int index = 0;
                    int OrderBy = 0;
                    PublishReff = "<table>" + PublishReff + "</table>";
                    System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                    xd.LoadXml(PublishReff);
                    System.Xml.XmlNodeList nodeList_tr = xd.GetElementsByTagName("tr");
                    foreach (System.Xml.XmlNode node_tr in nodeList_tr)
                    {
                        OrderBy++;
                        index = 0;
                        System.Xml.XmlNodeList nodeList_td = node_tr.SelectNodes("td");
                        foreach (System.Xml.XmlNode notde_td in nodeList_td)
                        {
                            if (index == 0)
                            {
                                index++;
                            }
                            else if (index == 1)
                            {
                                FormNo = notde_td.InnerText;
                                index++;
                            }
                            else if (index == 2)
                            {
                                FormName = notde_td.InnerText;
                                index++;
                            }
                            else if (index == 3)
                            {
                                PreservingDepartment = notde_td.Attributes["value"].Value;
                                index++;
                            }
                            else if (index == 4)
                            {
                                PreservingTime = String.IsNullOrEmpty(notde_td.InnerText.Trim()) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(notde_td.InnerText.Trim());
                                index++;
                            }
                            else if (index == 5)
                            {
                                FilePublishReff = notde_td.InnerText;
                                index++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        aPublishReffMN.InsertPublishReff(EditDocument, FormNo, FormName, PreservingDepartment, PreservingTime, FilePublishReff, "1", LoginSession.UserName(), OrderBy);
                    }
                    #endregion
                }
                #endregion

            }
            else
            {
                aInfo = "ERROR";
            }
            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = EditDocument;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeleteFileEdit(string FileName)
        {
            try
            {
                FileName = FileName.Trim();
                string filePath = string.Empty;
                filePath = Server.MapPath("~/Updatafile/PublishDoc/" + FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return JsonConvert.SerializeObject(FileName);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return JsonConvert.SerializeObject("Error");
            }
        }

        [WebMethod]
        public string CheckDisplaySubmitEditDocument(string EditDocument)
        {
            return JsonConvert.SerializeObject(aRegisterEditDocumentMN.CheckDisplaySubmitEditDocument(EditDocument, LoginSession.UserName()));
        }

        [WebMethod]
        public string AcceptRegisterEditDocument(string EditDocument, string States, string Comment, string Url,string DepartmentCheck)
        {
            #region
            UsersMN aUsersMN = new UsersMN();
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("B01"))
            {
                States = "B25";
                //DataTable aCheck = aUsersMN.CheckLevel(LoginSession.UserName());
                //if (aCheck.Rows.Count > 0)
                //{
                //    string Position = aCheck.Rows[0]["Position"].ToString().Trim();
                //    if (Position.Equals("TT"))
                //    {
                //        States = "B05";
                //    }
                //    else if (Position.Equals("TP"))
                //    {
                //        States = "B10";
                //    }
                //    else
                //    {
                //        States = "B03";
                //    }
                //}
                //else
                //{
                //    States = "B03";
                //}
            }
            //else if (States.Equals("B03"))
            //{
            //    int TS = aRegisterEditDocumentMN.CheckApprovalSection_EditDocument(EditDocument, States);
            //    if (TS > 1)
            //    {
            //        States = "B03";
            //    }
            //    else
            //    {
            //        States = "B05";
            //    }
            //}
            //else if (States.Equals("B05"))
            //{
            //    int TS = aRegisterEditDocumentMN.CheckApprovalSection_EditDocument(EditDocument, States);
            //    if (TS > 1)
            //    {
            //        States = "B05";
            //    }
            //    else
            //    {
            //        States = "B10";
            //    }
            //}
            //else if (States.Equals("B10"))
            //{
            //    if (String.IsNullOrEmpty(DepartmentCheck))
            //    {
            //        States = "B20";
            //    }
            //    else
            //    {
            //        States = "B15";
            //    }
                
            //}
            //else if (States.Equals("B15"))
            //{
            //    int TS = aRegisterEditDocumentMN.CheckApprovalSection_EditDocument(EditDocument,States);
            //    if (TS > 1)
            //    {
            //        States = "B15";
            //    }
            //    else
            //    {
            //        States = "B20";
            //    }

            //}
            //else if (States.Equals("B20"))
            //{
            //    States = "B25";
            //}
            else if (States.Equals("B25"))
            {
                States = "B26";
            }
            #endregion

            #region
            aNum = aRegisterEditDocumentMN.AcceptRegisterEditDocument(EditDocument, LoginSession.UserName(), States);
            if (aNum > 0)
            {
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(EditDocument, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;
                    
                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    
                    DataTable aTemp = aRegisterEditDocumentMN.CheckListSendMail_Edit(EditDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("B26"))
                            {
                                Utils.ContentSendMail_Accept(EditDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else
                            {
                                Utils.ContentSendMail_WaitSign(EditDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;


                            aHistorySendMailMN.InsertHistorySendMail(EditDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
            #endregion
        }

        [WebMethod]
        public string RejectRegisterEditDocument(string EditDocument, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = B30 : Lam lai don
            aNum = aRegisterEditDocumentMN.RejectRegisterEditDocument(EditDocument, LoginSession.UserName(), "B30");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(EditDocument, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aRegisterEditDocumentMN.CheckListSendMail_Edit(EditDocument);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(EditDocument, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(EditDocument, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string DeletedRegisterEditDocument(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aRegisterEditDocumentMN.DeletedRegisterEditDocument(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }
        #endregion

        #region session
        [WebMethod]
        public string SetSession(string Menu)
        {
            if (String.IsNullOrEmpty(Menu))
            {
                HttpCookie aCookieMenu = HttpContext.Current.Request.Cookies["MENU"];
                if (aCookieMenu != null)
                {
                    aCookieMenu.Expires = DateTime.Now.AddHours(-4);
                }
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("MENU");
                aCookie.Values["Value"] = Menu;
                aCookie.Expires.AddHours(4);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            return JsonConvert.SerializeObject("SUCCESS");
        }

        [WebMethod]
        public string GetSession()
        {
            HttpCookie aCookieMenu = HttpContext.Current.Request.Cookies["MENU"];
            if (aCookieMenu != null)
            {
                return JsonConvert.SerializeObject(Convert.ToString(aCookieMenu.Values["Value"]));
            }
            else
            {
                return JsonConvert.SerializeObject(string.Empty);
            }
        }

        #endregion

        #region CheckingNotice
        CheckingNoticeMN aCheckingNoticeMN = new CheckingNoticeMN();

        [WebMethod]
        public string ListRegisterCheckingNotice(string PublishDocument, string CreatedBy, string DocumentNo, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aCheckingNoticeMN.ListRegisterCheckingNotice(PublishDocument, CreatedBy, DocumentNo, FromApplicationDate, ToApplicationDate));
        }

        [WebMethod]
        public string ListCheckingNotice(string CodeCheck, string CreatedBy, string States, string CheckWait, string Person, string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aCheckingNoticeMN.ListCheckingNotice(CodeCheck, CreatedBy, States, CheckWait, Person, FromApplicationDate, ToApplicationDate));
        }


        [WebMethod]
        public string InsertOrUpdateCheckingNotice(string ID, string CodeCheck, string PublishDocument, string DocumentNo,
            string sApplicationDate, string Department, string Director, string ApplicationSite)
        {
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);
            if (String.IsNullOrEmpty(ID))
            {
                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                CodeCheck = "SOP-F-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("CodeCheck", "CheckingNotice");
            }

            string aInfo = "ERROR";
            int aNum = aCheckingNoticeMN.InsertOrUpdateCheckingNotice(ID, CodeCheck, PublishDocument, DocumentNo, ApplicationDate, Department, Director, "F01", LoginSession.UserName());
            if (aNum > 0)
            {
                aNum = aCheckingNoticeMN.InsertApprovalSection_Checking(CodeCheck, LoginSession.UserName());
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }

            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = CodeCheck;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedCheckingNotice(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aCheckingNoticeMN.DeletedCheckingNotice(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string AcceptCheckingNotice(string CodeCheck, string States, string Comment, string Url,
            string Opinion, string Person, string sEstimateCloseDate, string Reason)
        {
            #region
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("F01"))
            {
                States = "F05";
            }
            else if (States.Equals("F05"))
            {
                States = "F10";
            }
            #endregion
            DateTime EstimateCloseDate = String.IsNullOrEmpty(sEstimateCloseDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sEstimateCloseDate);
            aNum = aCheckingNoticeMN.AcceptCheckingNotice(CodeCheck, LoginSession.UserName(), States, Opinion, Person, EstimateCloseDate, Reason);
            if (aNum > 0)
            {
                #region
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(CodeCheck, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region Gui email

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aCheckingNoticeMN.CheckListSendMail_Checking(CodeCheck);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("F05"))
                            {
                                Utils.ContentSendMail_WaitSign(CodeCheck, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else if (States.Equals("F10"))
                            {
                                Utils.ContentSendMail_Edit(CodeCheck, Url, HoTen, UserName, ref Title, ref Contents);
                            }

                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;
                            aHistorySendMailMN.InsertHistorySendMail(CodeCheck, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());
                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
                #endregion
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string RejectCheckingNotice(string CodeCheck, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = F20 : Lam lai don
            aNum = aCheckingNoticeMN.RejectCheckingNotice(CodeCheck, LoginSession.UserName(), "F20");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(CodeCheck, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aCheckingNoticeMN.CheckListSendMail_Checking(CodeCheck);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(CodeCheck, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(CodeCheck, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string CheckDisplaySubmitCheckingNotice(string CodeCheck)
        {
            return JsonConvert.SerializeObject(aCheckingNoticeMN.CheckDisplaySubmitCheckingNotice(CodeCheck, LoginSession.UserName()));
        }

        #endregion

        #region RenewalsDocument
        RenewalsDocumentMN aRenewalsDocumentMN = new RenewalsDocumentMN();

        [WebMethod]
        public string ListRegisterRenewalDocument(string Code, string DCC, string Type)
        {
            return JsonConvert.SerializeObject(aRenewalsDocumentMN.ListRegisterRenewalDocument(Code, DCC, Type));
        }

        [WebMethod]
        public string ListRenewalsDocument(string RenewalCode, string CreatedBy, string States, string CheckWait, string DocumentNo,
            string fApplicationDate, string tApplicationDate)
        {
            DateTime FromApplicationDate = String.IsNullOrEmpty(fApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(fApplicationDate);
            DateTime ToApplicationDate = String.IsNullOrEmpty(tApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(tApplicationDate);

            return JsonConvert.SerializeObject(aRenewalsDocumentMN.ListRenewalsDocument(RenewalCode, CreatedBy, States, CheckWait, DocumentNo, FromApplicationDate, ToApplicationDate));
        }


        [WebMethod]
        public string InsertOrUpdateRenewalsDocument(string ID, string RenewalCode, string sApplicationDate, string ApplicationSite, string TypeRenewal,
            string DocumentNo, string DCC_NO, string Reason, string Revisor, string sCloseDate, string Department, string BeforRevised,
            string BeforRevisor, string BeforCloseDate, string Type)
        {
            DateTime ApplicationDate = String.IsNullOrEmpty(sApplicationDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sApplicationDate);
            DateTime EffectiveDate = Convert.ToDateTime("1900/01/01");
            DateTime CloseDate = String.IsNullOrEmpty(sCloseDate) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(sCloseDate);

            if (String.IsNullOrEmpty(ID))
            {
                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.ListCategorys(ApplicationSite, string.Empty, "CT-00003");
                string MaXuong = string.Empty;
                if (aData.Rows.Count > 0)
                {
                    MaXuong = Convert.ToString(aData.Rows[0]["Code"]);
                }
                RenewalCode = "SOP-H-" + MaXuong + DateTime.Now.Year.ToString() + Utils.GetCodeAuto("RenewalCode", "RenewalsDocument");
            }

            string aInfo = "ERROR";
            int aNum = aRenewalsDocumentMN.InsertOrUpdateRenewalsDocument(ID, RenewalCode, ApplicationDate, ApplicationSite, EffectiveDate, TypeRenewal, DocumentNo,
            DCC_NO, Reason, Revisor, CloseDate, "H01", Department, LoginSession.UserName(), BeforRevised, BeforRevisor, BeforCloseDate, Type);
            if (aNum > 0)
            {
                aNum = aRenewalsDocumentMN.InsertApprovalSection_Renewal(RenewalCode, LoginSession.UserName(), Department);
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }

            if (aInfo.Equals("SUCCESS"))
            {
                aInfo = RenewalCode;
            }

            return JsonConvert.SerializeObject(aInfo); ;
        }

        [WebMethod]
        public string DeletedRenewalsDocument(string ID)
        {
            string aInfo = string.Empty;
            int aNum = aRenewalsDocumentMN.DeletedRenewalsDocument(ID, LoginSession.UserName());
            if (aNum > 0)
            {
                aInfo = "SUCCESS";
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string AcceptRenewalsDocument(string RenewalCode, string States, string Comment, string Url, string Person)
        {
            Person = (Person.Equals("null") || Person.Equals("ALL")) ? string.Empty : Person;

            #region
            string aInfo = string.Empty;
            int aNum = 0;
            if (States.Equals("H01"))
            {
                States = "H05";
            }
            else if (States.Equals("H05"))
            {
                States = "H10";
            }
            else if (States.Equals("H10"))
            {
                if (String.IsNullOrEmpty(Person))
                {
                    States = "H20";
                }
                else
                {
                    States = "H15";
                }

            }
            else if (States.Equals("H15"))
            {
                States = "H20";
            }
            #endregion
            aNum = aRenewalsDocumentMN.AcceptRenewalsDocument(RenewalCode, LoginSession.UserName(), States, Person);
            if (aNum > 0)
            {
                #region
                //Status = C-00010  : Da ky
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(RenewalCode, LoginSession.UserName(), Comment, "C-00010");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region Gui email

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aRenewalsDocumentMN.CheckListSendMail_Renewal(RenewalCode);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            if (States.Equals("H20"))
                            {
                                Utils.ContentSendMail_Accept(RenewalCode, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            else
                            {
                                Utils.ContentSendMail_WaitSign(RenewalCode, Url, HoTen, UserName, ref Title, ref Contents);
                            }
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;
                            aHistorySendMailMN.InsertHistorySendMail(RenewalCode, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());
                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
                #endregion
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string RejectRenewalsDocument(string RenewalCode, string Comment, string Url)
        {
            string aInfo = string.Empty;
            int aNum = 0;
            //States = F20 : Lam lai don
            aNum = aRenewalsDocumentMN.RejectRenewalsDocument(RenewalCode, LoginSession.UserName(), "H25");
            if (aNum > 0)
            {
                //Status = C-00011  : Tu choi
                aNum = aRegisterCodeDocumentMN.UpdateApprovalSection(RenewalCode, LoginSession.UserName(), Comment, "C-00011");
                if (aNum > 0)
                {
                    aInfo = "SUCCESS";
                    #region

                    string ToEmail = string.Empty;
                    string BCC = string.Empty;
                    string Status = string.Empty;
                    string Contents = string.Empty;
                    string Title = string.Empty;


                    HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
                    UsersMN aUsersMN = new UsersMN();
                    DataTable aTemp = aRenewalsDocumentMN.CheckListSendMail_Renewal(RenewalCode);
                    for (int i = 0; i < aTemp.Rows.Count; i++)
                    {
                        string UserName = Convert.ToString(aTemp.Rows[i]["UserName"]);
                        DataTable aData = aUsersMN.ListUsers(UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "0");
                        for (int j = 0; j < aData.Rows.Count; j++)
                        {
                            string HoTen = Convert.ToString(aData.Rows[j]["HoTen"]);
                            Utils.ContentSendMail_Reject(RenewalCode, Url, HoTen, UserName, ref Title, ref Contents);
                            ToEmail = Convert.ToString(aData.Rows[j]["Email"]);
                            bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                            Status = bS ? "SUCCESS" : "ERRER";
                            ToEmail = ToEmail + "___" + UserName;

                            aHistorySendMailMN.InsertHistorySendMail(RenewalCode, ToEmail, BCC, Status, Contents, Title, LoginSession.UserName());

                        }
                    }
                    #endregion
                }
                else
                {
                    aInfo = "ERROR";
                }
            }
            else
            {
                aInfo = "ERROR";
            }
            return JsonConvert.SerializeObject(aInfo);
        }

        [WebMethod]
        public string CheckDisplaySubmitRenewalsDocument(string RenewalCode)
        {
            return JsonConvert.SerializeObject(aRenewalsDocumentMN.CheckDisplaySubmitRenewalsDocument(RenewalCode));
        }

        #endregion

    }
}
