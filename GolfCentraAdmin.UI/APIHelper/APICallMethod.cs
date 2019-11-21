using GolfCentraAdmin.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace GolfCentraAdmin.UI.APIHelper
{
    /// <summary>
    /// API Calling Method
    /// </summary>
    public class APICallMethod
    {
        /// <summary>
        /// APIClient View Model Details
        /// </summary>
        /// <returns></returns>
        public ViewModel.ApiClientViewModel GetApiClientModel()
        {
            string id = "";
            if (HttpContext.Current.Session[Constants.SessionName] != null)
            {
              ViewModel.EmployeeViewModel dbuser = (( ViewModel.EmployeeViewModel)HttpContext.Current.Session[Constants.SessionName]);

                id = dbuser.UniqueSessionId;
            }
            return new GolfCentraAdmin.ViewModel.ApiClientViewModel()
            {

                UserName = Common.Constants.ApiAccess.UserName,
                Password = Common.Constants.ApiAccess.Password,

                UniqueSessionId = id
            };
        }

        /// <summary>
        /// Http Client For API Access
        /// </summary>
        /// <returns> model Of Type HttpClient</returns>
        public HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            Int32 time = Convert.ToInt32(Common.Constants.ApiAccess.HttpTime);
            client.Timeout = TimeSpan.FromMinutes(time);
            client.BaseAddress = new Uri(Common.Constants.Url.WebApiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));
            return client;
        }

        /// <summary>
        /// Call API Method Using Rest Client For Single Model
        /// </summary>
        /// <param name="functionName">Method Name of API</param>
        /// <param name="controllerName"> Controller Name Of API</param>
        /// <param name="model"> Model Of Data</param>
        /// <returns>Response From API</returns>
        public HttpResponseMessage GetHttpResponseMessage(string functionName, string controllerName, dynamic model)
        {
            try
            {
                model.ApiClientViewModel = GetApiClientModel();
                return GetHttpClient().PostAsJsonAsync("api/" + controllerName + "/" + functionName, (object)model).Result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// Call API Method Using Rest Client For List Model
        /// </summary>
        /// <param name="functionName">Method Name of API</param>
        /// <param name="controllerName"> Controller Name Of API</param>
        /// <param name="model"> Model Of Data</param>
        /// <returns>Response From API</returns>
        public HttpResponseMessage GetHttpResponseMessageList(string functionName, string controllerName, dynamic model)
        {
            try
            {
                model[0].ApiClientViewModel = GetApiClientModel();
                return GetHttpClient().PostAsJsonAsync("api/" + controllerName + "/" + functionName, (object)model).Result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public HttpResponseMessage GetHttpResponseMessage(string functionName, string controllerName, dynamic model,string token)
        {
            try
            {
                model.ApiClientViewModel = new GolfCentraAdmin.ViewModel.ApiClientViewModel();
                model.ApiClientViewModel.UserName = Common.Constants.ApiAccess.UserName;
                model.ApiClientViewModel.Password = Common.Constants.ApiAccess.Password;
                model.ApiClientViewModel.UniqueSessionId = token;
                return GetHttpClient().PostAsJsonAsync("api/" + controllerName + "/" + functionName, (object)model).Result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}