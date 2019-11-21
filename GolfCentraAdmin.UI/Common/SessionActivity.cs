using GolfCentraAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace GolfCentraAdmin.UI.Common
{
    public class SessionActivity
    {

        public void SaveSessionActivity(string loginHistoryId,string controllerName,string actionName,string performOn )
        {
            SessionActivityPageViewModel sessionActivityPageViewModel = new SessionActivityPageViewModel() {
               
                ControllerName=controllerName,
                ActionName=actionName,
                PerformOn = performOn
            };
             HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveSessionActivity", "AdminPanel", sessionActivityPageViewModel);
            if (response.IsSuccessStatusCode)
            { }

        }
    }
}