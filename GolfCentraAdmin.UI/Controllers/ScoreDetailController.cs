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
    public class ScoreDetailController : MyBaseController
    {
        // GET: ScoreDetail
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search Scorew Details By Advance Search
        /// </summary>
        /// <param name="scoreSearchViewModel">Model Of Type BookingVie Model</param>
        /// <returns>Score Details In String Format</returns>
        public JsonResult GetScoreDetailByAdvanceSearch(ScoreSearchViewModel scoreSearchViewModel)
        {
          
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetScoreDetailByAdvanceSearch", "AdminPanel", scoreSearchViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ScoreDetailsViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ScoreDetailsViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/ScoreDetail/_ScoreDetails.cshtml", dbBookingDetails.Content);

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