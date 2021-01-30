using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models
{
    public class ConnectionSQL
    {
        SqlConnection aSqlConnection = new SqlConnection(DbHelperSQL.ConnectionString);
        public SqlConnection Connection()
        {
            return aSqlConnection;
        }
        public void Open()
        {
            try
            {
                string aState = aSqlConnection.State.ToString();
                if (aState.Trim().Equals("Closed"))
                {
                    aSqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void Close()
        {
            try
            {
                string aState = aSqlConnection.State.ToString();
                if (aState.Trim().Equals("Open"))
                {
                    aSqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

    }
}