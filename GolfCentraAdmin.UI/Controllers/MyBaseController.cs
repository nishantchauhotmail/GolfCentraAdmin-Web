using GolfCentraAdmin.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GolfCentraAdmin.UI.Controllers
{
    public class MyBaseController : Controller
    {
        /// <summary>
        /// OverRide Controller Action Method For Login Check
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    bool flag = true;
                    if (filterContext.HttpContext.Session.Count > 0)
                    {
                        if (filterContext.HttpContext.Session[Constants.SessionName] != null)
                            flag = false;
                        if (Session["SFToken"] != null && Request.Cookies["SFToken"] != null)
                        {
                            if (!Session["SFToken"].ToString().Equals(Request.Cookies["SFToken"].Value))
                            { flag = true; }

                        }
                        else
                        {
                            flag = true;
                        }

                    }
                    if (!flag)
                    {
                        string controllerName = ControllerContext.RouteData.Values["Controller"].ToString();
                        string actionName = ControllerContext.RouteData.Values["Action"].ToString();
                        string id;
                        if (ControllerContext.RouteData.Values["id"] + Request.Url.Query == "")
                        {
                            id = ControllerContext.RouteData.Values["id"] + Request.Url.Query;
                        }
                        else
                        {
                            id = ControllerContext.RouteData.Values["id"] + Request.QueryString[0];
                        }
                        //Check
                        List<GolfCentraAdmin.ViewModel.PageViewModel> pageviewModel = (List<GolfCentraAdmin.ViewModel.PageViewModel>)HttpContext.Session["AllMenu"];
                        List<GolfCentraAdmin.ViewModel.PageViewModel> p = pageviewModel.Where(x => x.ControllerName.ToLower() == controllerName.ToLower() && x.ActionName.ToLower() == actionName.ToLower()).ToList();
                        if (p != null && p.Count() != 0)
                        {
                            List<GolfCentraAdmin.ViewModel.PageViewModel> pageviewModel1 = (List<GolfCentraAdmin.ViewModel.PageViewModel>)HttpContext.Session["NavigationMenu"];
                            List<GolfCentraAdmin.ViewModel.PageViewModel> p1 = pageviewModel1.Where(x => x.ControllerName.ToLower() == controllerName.ToLower() && x.ActionName.ToLower() == actionName.ToLower()).ToList();
                            if (p1 != null && p1.Count() != 0)
                            {

                            }
                            else
                            {
                                filterContext.Result = (ActionResult)this.Redirect(Constants.Url.WebSiteUrl + "Dashboard/Index");
                            }
                        }
                        ViewModel.EmployeeViewModel employeeViewModel = (ViewModel.EmployeeViewModel)HttpContext.Session[Constants.SessionName];
                        new Common.SessionActivity().SaveSessionActivity(employeeViewModel.UniqueSessionId, controllerName, actionName, id);
                        return;
                    }
                    this.Session.RemoveAll();
                    filterContext.Result = (ActionResult)this.Redirect(Constants.Url.WebSiteUrl + "LogOut/Index");
                }
                else
                {
                    bool flag = true;
                    if (filterContext.HttpContext.Session.Count > 0)
                    {
                        if (filterContext.HttpContext.Session[Constants.SessionName] != null)
                            flag = false;
                        if (Session["SFToken"] != null && Request.Cookies["SFToken"] != null)
                        {
                            if (!Session["SFToken"].ToString().Equals(Request.Cookies["SFToken"].Value))
                            { flag = true; }
                            
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        string controllerName = ControllerContext.RouteData.Values["Controller"].ToString();
                        string actionName = ControllerContext.RouteData.Values["Action"].ToString();
                        string id;
                        if (ControllerContext.RouteData.Values["id"] + Request.Url.Query == "")
                        {
                            id = ControllerContext.RouteData.Values["id"] + Request.Url.Query;
                        }
                        else
                        {
                            id = ControllerContext.RouteData.Values["id"] + Request.QueryString[0];
                        }

                        ViewModel.EmployeeViewModel employeeViewModel = (ViewModel.EmployeeViewModel)HttpContext.Session[Constants.SessionName];
                        new Common.SessionActivity().SaveSessionActivity(employeeViewModel.UniqueSessionId, controllerName, actionName, id);
                        return;
                    }
                    this.Session.RemoveAll();
                    filterContext.Result = (ActionResult)this.Json(new { code = -99, message = "Please Login Again" });
                }
            }
            catch (Exception ex)
            {
                this.Session.RemoveAll();
                filterContext.Result = (ActionResult)this.Redirect(Constants.Url.WebSiteUrl + "LogOut/Index");
            }
        }
    }
}