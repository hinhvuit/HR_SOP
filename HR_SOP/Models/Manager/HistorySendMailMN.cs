using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class HistorySendMailMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public int InsertHistorySendMail(string Code, string ToEmail, string BCC, string Status, string Contents, string Title, string CreatedBy)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into HistorySendMail(ID,Code,ToEmail,BCC,Status,Contents,Title,CreatedBy,CreatedDate)"
                                + " values(NEWID(), @Code, @ToEmail, @BCC, @Status, @Contents, @Title, @CreatedBy, GETDATE())";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@ToEmail", ToEmail);
                cmd.Parameters.AddWithValue("@BCC", BCC);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Contents", Contents);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}