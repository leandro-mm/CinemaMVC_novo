using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Helpers
{
    public static class ErrorHelper
    {
        public static string GetErrorMessage(Exception ex)
        {
            var erro = ex.Message;
            var msg1 = ex.InnerException != null ? ex.InnerException.Message : "";
            var msg2 = ex.InnerException != null ? (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "") : "";
            return erro + " " + msg1 + " " + msg2;
        }       
    }
}