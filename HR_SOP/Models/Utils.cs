using HR_SOP.Models.Manager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HR_SOP.Models
{
    public static class Utils
    {
        public static string GetCodeAuto(string NameCol, string NameTable)
        {
            try
            {
                SqlConnection ds_conn = new SqlConnection(DbHelperSQL.ConnectionString);
                ds_conn.Open();
                DataTable aTemp = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ds_conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CreatedCodeAuto";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@NameCol", NameCol);
                cmd.Parameters.AddWithValue("@NameTable", NameTable);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aTemp);
                ds_conn.Close();

                if (aTemp.Rows.Count > 0)
                {
                    return Convert.ToString(aTemp.Rows[0]["Code"]);
                }
                else
                {
                    return Convert.ToString(Guid.NewGuid()).Substring(1, 10);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Convert.ToString(Guid.NewGuid()).Substring(1, 10);
            }
        }

        public static string MD5(string aText)
        {
            MD5CryptoServiceProvider aMd5 = new MD5CryptoServiceProvider();
            UTF8Encoding aEncoding = new UTF8Encoding();
            StringBuilder sb = new StringBuilder();
            byte[] aByte = aMd5.ComputeHash(aEncoding.GetBytes(aText));
            for (int i = 0; i < aByte.Length; i++)
            {
                sb.Append(aByte[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string Encrypt(string UserName)
        {
            try
            {
                byte[] keyArray;
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                UTF8Encoding aEncoding = new UTF8Encoding();
                keyArray = hashmd5.ComputeHash(aEncoding.GetBytes("HIEN1990"));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] toEncryptArray = aEncoding.GetBytes(UserName);
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray)+"hien";
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        public static string Decrypt(string UserName)
        {
            try
            {
                UserName = UserName.Substring(0, UserName.Length - 4);
                byte[] keyArray;
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                UTF8Encoding aEncoding = new UTF8Encoding();
                keyArray = hashmd5.ComputeHash(aEncoding.GetBytes("HIEN1990"));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] toEncryptArray = Convert.FromBase64String(UserName);
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return aEncoding.GetString(resultArray);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        public static bool SendMail(string sendto, string bccto, string copyto, string title, string body)
        {
            try
            {
                CategorysMN aCategorysMN = new CategorysMN();
                DataTable aData = aCategorysMN.GetCategorysByCode("EMAIL");
                if (aData.Rows.Count > 0)
                {
                    MailFlow.SMTPMail aMailflow = new MailFlow.SMTPMail();
                    return aMailflow.MailSend("vnesop6688!@!@", Convert.ToString(aData.Rows[0]["CatName"]).Trim(), sendto, copyto, bccto, title, body);
                    //return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ContentSendMail_WaitSign(string Code, string url, string Names, string UserName, ref string title, ref string body)
        {
            title = "【中央文管系統消息】尊敬的 " + Names + " 主管，您好！您有  1 份文件待簽核.單號為：" + Code;
            
            body = "----------------------------------------------------------";
            body += Environment.NewLine;
            body += "尊敬的 " + Names + " 主管：";
            body += "您好， 這是中央文管系統發出的簽核溫馨提醒。";
            body += "有一份文件電單需要您簽核。";
            body += "請點擊以下系統鏈接簽核： ";
            body += Environment.NewLine;
            body += url + "&id_user=" + Encrypt(UserName);
            body += Environment.NewLine;
            body += "此郵件由系統自動發送，請勿直接回复！";
            body += "若有疑問請聯繫對接窗口。";
            body += "TEL:535-20222";
            body += "EMAIL:chr-vn-planning-01@mail.foxconn.com";
            body += Environment.NewLine;
            body += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            body += Environment.NewLine;
            body += "----------------------------------------------------------";
            
        }

        public static void ContentSendMail_Accept(string Code, string url, string Names, string UserName, ref string title, ref string body)
        {

            title = "【中央文管系統消息】，尊敬的 " + Names + " 主管，單號：" + Code + "，【文件清單】已被批准";

            body = "----------------------------------------------------------";
            body += Environment.NewLine;
            body += "您好！";
            body += "單號：" + Code + "，【文件清單】已被批准";
            body += "請點擊以下系統鏈接查看簽核進度： ";
            body += Environment.NewLine;
            body += url + "&id_user=" + Encrypt(UserName);
            body += Environment.NewLine;
            body += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            body += Environment.NewLine;
            body += "----------------------------------------------------------";
            
        }

        public static void ContentSendMail_Edit(string Code, string url, string Names, string UserName, ref string title, ref string body)
        {
            title = "【中央文管系統消息】尊敬的 " + Names + " 主管，您好！您有  1 份文件到期需修改.單號為：" + Code;

            body = "----------------------------------------------------------";
            body += Environment.NewLine;
            body += "尊敬的 " + Names + " 主管：";
            body += "您好， 這是中央文管系統發出的修改提醒。";
            body += "有一份文件電單需要您修改。";
            body += "請點擊以下系統鏈接修改： ";
            body += Environment.NewLine;
            body += url + "&id_user=" + Encrypt(UserName);
            body += Environment.NewLine;
            body += "此郵件由系統自動發送，請勿直接回复！";
            body += "若有疑問請聯繫對接窗口。";
            body += "TEL:535-20222";
            body += "EMAIL:chr-vn-planning-01@mail.foxconn.com";
            body += Environment.NewLine;
            body += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            body += Environment.NewLine;
            body += "----------------------------------------------------------";
        }

        public static void ContentSendMail_Reject(string Code, string url, string Names, string UserName, ref string title, ref string body)
        {

            title = "【中央文管系統消息】，尊敬的 " + Names + " 主管，單號：" + Code + "，【文件清單】已被拒絕";

            body = "----------------------------------------------------------";
            body += Environment.NewLine;
            body += "您好！";
            body += "單號：" + Code + "，【文件清單】已被拒絕";
            body += "請點擊以下系統鏈接查看簽核進度： ";
            body += Environment.NewLine;
            body += url + "&id_user=" + Encrypt(UserName);
            body += Environment.NewLine;
            body += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            body += Environment.NewLine;
            body += "----------------------------------------------------------";
            
        }

        public static void ContentSendMail_AutoSend(string Code, string url, string Names, string UserName, ref string title, ref string body)
        {
            title = "【中央文管系統消息】尊敬的 " + Names + " 主管，您好！您有  1 份文件到期需修改.單號為：" + Code;

            body = "----------------------------------------------------------";
            body += Environment.NewLine;
            body += "尊敬的 " + Names + " 主管：";
            body += "您好， 這是中央文管系統發出的修改提醒。";
            body += "有一份文件電單需要您修改。";
            body += "請點擊以下系統鏈接修改： ";
            body += Environment.NewLine;
            body +=  url + "&id_user=" + Encrypt(UserName);
            body += Environment.NewLine;
            body += "此郵件由系統自動發送，請勿直接回复！";
            body += "若有疑問請聯繫對接窗口。";
            body += "TEL:535-20222";
            body += "EMAIL:chr-vn-planning-01@mail.foxconn.com";
            body += Environment.NewLine;
            body += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            body += Environment.NewLine;
            body += "----------------------------------------------------------";   

        }
    }
}