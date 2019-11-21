using GolfCentraAdmin.UI.Common;
using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static GolfCentraAdmin.UI.Common.MyAttribute;

namespace GolfCentraAdmin.UI.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Index()
        {
            try
            {
                //If User Logged Redirect To Dashboard
                if (this.HttpContext.Session[Constants.SessionName] != null)
                    return (ActionResult)this.RedirectToAction("Index", "DashBoard");
               

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
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        [Throttle(Name = "TestThrottle", Message = "You must wait {n} seconds before accessing this url again.", Seconds = 1)]
        [ValidateAntiForgeryToken]
        /// <summary>
        /// Login Method 
        /// </summary>
        /// <param name="employeeViewModel">Model Of Type EmployeeViewModel</param>
        /// <returns>Return Suceess Or Failed</returns>
        public ActionResult DoLogin(EmployeeViewModel employeeViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("Login", "AdminPanel", employeeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.EmployeeViewModel> dbuser = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.EmployeeViewModel>>(data);
                if (dbuser.IdentityResult.Status == true)
                {
                    if (dbuser.Content.IsFirstLogIn == false)
                    {
                        string guid = Guid.NewGuid().ToString();
                        Session["SFToken"] = guid;
                        // now create a new cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("SFToken", guid));
                        HttpContext.Session.Add(Common.Constants.SessionName, dbuser.Content);
                        Session["NavigationMenu"] = dbuser.Content.PageViewModels;
                        Session["AllMenu"] = dbuser.Content.AllPageViewModels;

                        return Json(new { code = 0, message = "success" });
                    }
                    else
                    {
                        TempData["session"] = dbuser.Content;
                        return Json(new { code = -22, message = "" });
                    }
                }
                else
                {
                    return Json(new { code = -1, message = dbuser.IdentityResult.Message });
                }
            }
            else
            {
                return Json(new { code = -2, message = "failed" });
            }

        }
    }
}