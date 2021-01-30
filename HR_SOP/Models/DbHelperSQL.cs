using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HR_SOP.Models
{
    public static class DbHelperSQL
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    }
}