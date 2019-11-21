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
    public class SessionDetailController : MyBaseController
    {
        // GET: SessionDetail
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get All Active Session Details
        /// </summary>
        /// <returns>Return Active Session Details In String Format </returns>
        public JsonResult GetAllSessionDetail()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllSessionDetail", "AdminPanel", new SessionMasterViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SessionDetail/_Search.cshtml", dbBookingDetails.Content);

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
        /// Save Session Details
        /// </summary>
        /// <param name="sessionMasterViewModel">Model Of Type SessionMasterViewModel</param>
        /// <returns>Return Success Or Failed</returns>
        public JsonResult SaveSessionDetail(SessionMasterViewModel sessionMasterViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveSessionDetail", "AdminPanel", sessionMasterViewModel);
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

        /// <summary>
        /// Update Session Details 
        /// </summary>
        /// <param name="sessionMasterViewModel">Model of Type SessionMasterViewModel</param>
        /// <returns>return Success Or Failed</returns>
        public JsonResult UpdateSessionDetail(SessionMasterViewModel sessionMasterViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateSessionDetail", "AdminPanel", sessionMasterViewModel);
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

        /// <summary>
        /// Update Session Details Popup
        /// </summary>
        /// <param name="SessionId">Id Of Session</param>
        /// <param name="StartTime">Start Time Of Session</param>
        /// <param name="EndTime">End Time Of Session</param>
        /// <param name="Name">Name Of Session</param>
        /// <returns>Return Detail For PopUp In String Format</returns>
        public JsonResult UpdateSessionPopUp(long SessionId, TimeSpan StartTime, TimeSpan EndTime, string Name)
        {
            SessionMasterViewModel sessionMasterViewModel = new SessionMasterViewModel()
            {
                SessionName = Name,
                SessionId = SessionId,
                StartTime = StartTime,
                EndTime = EndTime
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SessionDetail/_Update.cshtml", sessionMasterViewModel);

            return Json(new { code = 0, message = convertedData });

        }


        public JsonResult DeleteSessionPopUp(long sessionId)
        {
            SessionMasterViewModel sessionMasterViewModel = new SessionMasterViewModel()
            {
               
                SessionId = sessionId,
              
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SessionDetail/_Delete.cshtml", sessionMasterViewModel);

            return Json(new { code = 0, message = convertedData });

        }


        public JsonResult DeleteSession(long sessionId)
        {
            SessionMasterViewModel sessionMasterViewModel = new SessionMasterViewModel()
            {

                SessionId = sessionId,

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteSession", "AdminPanel", sessionMasterViewModel);
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