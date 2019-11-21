using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace GolfCentraAdmin.UI.Controllers
{
    public class MaritalStatusDetailController : MyBaseController
    {
        // GET: MaritalStatusDetail
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get All Marital status Detail
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMaritalStatusType()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMaritalStatusType", "AdminPanel", new MaritalStatusViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MaritalStatusViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MaritalStatusViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/MaritalStatusDetail/_MaritalStatusDetail.cshtml", dbBookingDetails.Content);

                    return Json(new { code = 0, message = convertedData });
                }
                else
                {
                    if (dbBookingDetails.IdentityResult.Message == "Invalid access details, Please log-out.")
                    {
                        return Json(new { code = -99, message = "Please Login Again" });
                    }
                    else
                    {
                        return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                    }
                }
            }
            else
            {
                return Json(new { code = -2, message = "failed" });
            }

        }
    }
}