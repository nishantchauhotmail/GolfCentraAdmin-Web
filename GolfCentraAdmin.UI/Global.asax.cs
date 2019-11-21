using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GolfCentraAdmin.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
           
        }

        protected void Application_PreSendRequestHeaders()
        {
            Request.Headers.Remove("X-AspNetMvc-Version");
            Request.Headers.Remove("Server");
            Request.Headers.Remove("X-AspNet-Version");
            Request.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();



            // Log the exception and notify system operators
            //    ExceptionUtility.LogException(exc, "DefaultPage");
            //  ExceptionUtility.NotifySystemOps(exc);

            //  return ViewPage();
            // Clear the error from the server

            Server.ClearError();
            this.Response.RedirectToRoute("Default", new { controller = "Account", action = "Error" });

        }
    }
}
