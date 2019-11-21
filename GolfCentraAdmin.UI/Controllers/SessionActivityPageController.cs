using GolfCentraAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GolfCentraAdmin.UI.Controllers
{
    public class SessionActivityPageController : MyBaseController
    {
        // GET: SessionActivityPage
        public ActionResult Index()
        {

            Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

            List<SessionActivityPageViewModel> sessionActivityPageViewModel = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new SessionActivityPageViewModel { ControllerName = x.DeclaringType.Name, ActionName = x.Name })
                    .ToList();
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessageList("AddSessionActivityPage", "AdminPanel", sessionActivityPageViewModel);
            if (response.IsSuccessStatusCode)
            { }

            return View();
        }
    }
}