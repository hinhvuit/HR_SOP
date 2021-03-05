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
using HR_SOP.Models.Manager;

namespace HR_SOP.Models
{
    public class GetHRData
    {
        HRData.YNSafety abc = new HRData.YNSafety();
        UsersMN auserNN = new UsersMN();

        public HRData.SoapHead SoapHead() {
            HRData.SoapHead soapHead = new HRData.SoapHead();
            soapHead.UserName = "YNSafety_User";
            soapHead.Password = "YNSafety$User_56#";
            return abc.SoapHeadValue = soapHead;
        }
        public bool CheckData(string employeeno)
        {
            SoapHead();
            DataTable check = null;
            try
            {
                check = abc.GetEmpInfo_Dt(employeeno);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            if (check.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GetDataHR(string empno)
        {
            SoapHead();
            DataTable test = null;
            try
            {
                bool a = abc.UserValidate();
                test = abc.GetEmpInfo_Dt(empno);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            if (test.Rows.Count != 0)
            {

            }
        }
    }
}