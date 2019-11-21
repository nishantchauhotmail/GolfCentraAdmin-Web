using GolfCentraAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GolfCentraAdmin.UI.Controllers
{
    public class LogoutController : Controller
    {
        /// <summary>
        /// Logged Out User And Clear Session
        /// </summary>
        /// <returns>Redirect To Login Page</returns>
        public ActionResult Index()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("Logout", "AdminPanel", new EmployeeViewModel());
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
 
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Request.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                
            }

            if (Request.Cookies["SFToken"] != null)
            {
                Response.Cookies["SFToken"].Value = string.Empty;
                Request.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
               
            }
            return RedirectToAction("Index", "Login");

        }
    }
}