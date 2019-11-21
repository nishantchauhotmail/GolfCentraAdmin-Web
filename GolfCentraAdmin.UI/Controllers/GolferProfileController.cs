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
    public class GolferProfileController : MyBaseController
    {
        // GET: GolferProfile
        public ActionResult Index()
        {
            List<GolferViewModel> golferViewModels = new List<GolferViewModel>();
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllGolferProfile", "AdminPanel", new GolferViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GolferViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GolferViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    golferViewModels = dbBookingDetails.Content;
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
            return View(golferViewModels);
            
        }

        /// <summary>
        /// Get Golfer Details By Golfer Id
        /// </summary>
        /// <param name="golferId">Id Of Golfer</param>
        /// <returns> Return String For POPUP</returns>
        public JsonResult GetUserDetailByGolferId(long golferId)
        {
            GolferViewModel golferViewModel = new GolferViewModel()
            {
                GolferId = golferId
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetGolferProfileByGolferId", "AdminPanel", golferViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.GolferViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.GolferViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/GolferProfile/_GolferDetailPopUp.cshtml", dbBookingDetails.Content);

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

        /// <summary>
        /// Edit Golfer Details
        /// </summary>
        /// <param name="golferViewModel"> Model  Of GolferViewModel </param>
        /// <returns>Return True Or False</returns>
        public JsonResult EditGolferDetail(GolferViewModel golferViewModel)
        {
            golferViewModel.UpdateId = 1;
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateGolferProfile", "AdminPanel", golferViewModel);
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