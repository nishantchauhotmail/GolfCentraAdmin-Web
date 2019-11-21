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
    public class ScoreManagementController : MyBaseController
    {
        // GET: ScoreManagement
        public ActionResult Index()
        {
            ScoreSearchViewModel scoreSearchViewModel = new ScoreSearchViewModel()
            {
                CoursePairingViewModels = CoursePairing()
            };
            return View(scoreSearchViewModel);
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetScoreDetailByAdvanceSearch(ScoreSearchViewModel scoreSearchViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetScoreDetailReportByAdvanceSearch", "AdminPanel", scoreSearchViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ScoreDetailsViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ScoreDetailsViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/ScoreManagement/_ScoreDetails.cshtml", dbBookingDetails.Content);

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

        private List<CoursePairingViewModel> CoursePairing()
        {
            List<CoursePairingViewModel> coursePairingViewModel = new List<CoursePairingViewModel>
            {
                new CoursePairingViewModel() { CoursePairingName = "Ridge 9 (9 Hole)", CoursePairingId = 4},
                new CoursePairingViewModel() { CoursePairingName = "Valley 9 (9 Hole)", CoursePairingId = 5 },
                new CoursePairingViewModel() { CoursePairingName = "Canyon 9 (9 Hole)", CoursePairingId = 6 },

                new CoursePairingViewModel() { CoursePairingName = "Ridge 9 - Valley 9(18 Hole)", CoursePairingId = 1 },
                new CoursePairingViewModel() { CoursePairingName = "Valley 9 - Canyon 9(18 Hole)", CoursePairingId = 2 },
                new CoursePairingViewModel() { CoursePairingName = "Canyon 9 - Ridge 9(18 Hole)", CoursePairingId = 3 }
            };
            return coursePairingViewModel;
        }

        public ActionResult EditScore(long scoreId)
        {
            ScoreDetailsViewModel scoreDetailsViewModel = new ScoreDetailsViewModel()
            {
                ScoreId = scoreId
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("CourseDetailForScorPost", "AdminPanel", scoreDetailsViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.ScoreDetailsViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.ScoreDetailsViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                
                    return View(dbBookingDetails.Content);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

            

           
        }
        [ValidateAntiForgeryToken]
        public JsonResult SaveScoreCard(ScoreDetailsViewModel scoreDetailsViewModel)
        {
           
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateScore", "AdminPanel", scoreDetailsViewModel);
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