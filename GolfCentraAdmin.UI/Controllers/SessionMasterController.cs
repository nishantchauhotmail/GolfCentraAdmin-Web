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
    public class SessionMasterController : MyBaseController
    {
        // GET: SessionMaster
        public ActionResult Index()
        {
            return View();
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult BookTeeTimeTab()
        {
            List<SessionMasterViewModel> sessionMasterViewModels = new List<SessionMasterViewModel>();
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllActiveSessionMaster", "AdminPanel", new SessionMasterViewModel() { BookingTypeId = 1 });
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    sessionMasterViewModels = dbBookingDetails.Content.ToList();
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SessionMaster/_TeeTimeTab.cshtml", dbBookingDetails.Content);
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
        ///  Get All Active Session Master whose Id=2
        /// </summary>
        /// <returns></returns>


        public JsonResult DrivingRangeTab()
        {
            List<SessionMasterViewModel> sessionMasterViewModels = new List<SessionMasterViewModel>();
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllActiveSessionMaster", "AdminPanel", new SessionMasterViewModel() { BookingTypeId = 2 });
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    sessionMasterViewModels = dbBookingDetails.Content.ToList();
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SessionMaster/_DrivingRangeTab.cshtml", dbBookingDetails.Content);

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

        public JsonResult SaveTeeTime(List<SessionMasterViewModel> sessionMasterViewModels )
        {
         
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessageList("UpdateSlotSessionDetailTeeTime", "AdminPanel", sessionMasterViewModels);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return Json(new { code = 0, message = true });
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


        public JsonResult SaveDrivingRange(List<SessionMasterViewModel> sessionMasterViewModels)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessageList("UpdateSlotSessionDetailDrivingRange", "AdminPanel", sessionMasterViewModels);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return Json(new { code = 0, message = true });
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