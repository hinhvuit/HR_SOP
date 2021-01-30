using HR_SOP.Models;
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
    public partial class AutoSendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region
            string Url = string.Empty;
            string ToEmail = string.Empty;
            string BCC = string.Empty;
            string Status = string.Empty;
            string Contents = string.Empty;
            string Title = string.Empty;
            string UserName = string.Empty;
            string FullName = string.Empty;
            string PublishDocument = string.Empty;
            HistorySendMailMN aHistorySendMailMN = new HistorySendMailMN();
            RegisterPublishDocumentMN aRegisterPublishDocumentMN = new RegisterPublishDocumentMN();
            DataTable aData = aRegisterPublishDocumentMN.CheckSendMailAuto();
            for (int i = 0; i < aData.Rows.Count; i++)
            {
                UserName = Convert.ToString(aData.Rows[i]["UserName"]);
                FullName = Convert.ToString(aData.Rows[i]["HoTen"]);
                ToEmail = Convert.ToString(aData.Rows[i]["Email"]);
                PublishDocument = Convert.ToString(aData.Rows[i]["PublishDocument"]);
                Url = Request.Url.OriginalString;
                Url = Url.Replace("AutoSendMail", "TransferEdit.aspx");
                Url = Url + "?PublishDocument=" + PublishDocument;
                Utils.ContentSendMail_AutoSend(PublishDocument, Url, FullName, UserName, ref Title, ref Contents);
                bool bS = Utils.SendMail(ToEmail, BCC, BCC, Title, Contents);
                Status = bS ? "SUCCESS" : "ERRER";
                ToEmail = ToEmail + "___" + UserName;
                aHistorySendMailMN.InsertHistorySendMail(PublishDocument, ToEmail, BCC, Status, Contents, Title, "System");
            }
            #endregion
        }
    }
}