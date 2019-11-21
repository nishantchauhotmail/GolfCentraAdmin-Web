using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GolfCentraAdmin.UI.Controllers
{
    public class TeeSheetController : MyBaseController
    {
        // GET: TeeSheet
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTeeSheetById(long coursePairingId,DateTime searchDate)
        {
            TeeSheetViewModel teeSheetViewModel = new TeeSheetViewModel()
            {
                CoursePairingId=coursePairingId,
                SearchDate= searchDate
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetTeeTimeSheet", "AdminPanel", teeSheetViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.TeeSheetViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.TeeSheetViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TeeSheet/_TeeSheet.cshtml", dbBookingDetails.Content);

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