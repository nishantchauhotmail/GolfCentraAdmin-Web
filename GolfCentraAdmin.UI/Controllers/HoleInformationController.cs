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
    public class HoleInformationController : MyBaseController
    {
        // GET: HoleInformation
        public ActionResult Index()
        {
            TempData["HoleNumberList"] = null;
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetHoleNumberList", "AdminPanel", new HoleViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.HoleViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.HoleViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["HoleNumberList"] = dbBookingDetails.Content;
                }
                else
                {
                    //  return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                // return Json(new { code = -2, message = "failed" });
            }
            return View();
        }

        /// <summary>
        /// Get Hole Details By Hole Number
        /// </summary>
        /// <param name="holeNumberId">Id Of Hole Number</param>
        /// <returns>Return Details In String with View</returns>
        public JsonResult GetHoleInformationByHoleNumberId(long holeNumberId)
        {
            HoleViewModel holeViewModel = new HoleViewModel()
            {
                HoleNumberId = holeNumberId
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetHoleDetailsByHoleNumberId", "AdminPanel", holeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.HoleViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.HoleViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/HoleInformation/_HoleInformation.cshtml", dbBookingDetails.Content);

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

        [ValidateAntiForgeryToken]
        /// <summary>
        /// Save Hole Details
        /// </summary>
        /// <param name="holeViewModel">Model Of Type HoleViewModel</param>
        /// <returns>Return True Or False </returns>
        public JsonResult SaveHoleInformation(HoleViewModel holeViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveUpdateHoleInformation", "AdminPanel", holeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    return Json(new { code = 0, message = "Success" });
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