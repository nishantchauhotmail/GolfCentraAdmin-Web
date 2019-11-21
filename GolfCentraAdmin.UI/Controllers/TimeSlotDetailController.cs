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
    public class TimeSlotDetailController : MyBaseController
    {
        // GET: TimeSlotDetail
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get All Active Time Slot Details
        /// </summary>
        /// <returns> Return All Time Slot Details In String Format</returns>
        public JsonResult GetAllTimeSlot()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllTimeSlot", "AdminPanel", new SlotViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TimeSlotDetail/_TimeSlotDetail.cshtml", dbBookingDetails.Content);

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
        /// Save Time Slot Details
        /// </summary>
        /// <param name="timeSlot">New Time For Save</param>
        /// <returns>Return Success Or Failed</returns>
        public JsonResult SaveTimeSlot(TimeSpan timeSlot)
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                Time = timeSlot
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveSlotTimeDetail", "AdminPanel", slotViewModel);
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
        ///  Update Time Slot Details
        /// </summary>
        /// <param name="slotId">Id Of Slot</param>
        /// <param name="timeSlot">Time Of Slot</param>
        /// <returns> Return Success Or Failed</returns>
        public JsonResult UpdateTimeSlot(long slotId, TimeSpan timeSlot)
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                Time = timeSlot,
                SlotId = slotId
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateTimeSlot", "AdminPanel", slotViewModel);
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
        /// Delete Time Slot
        /// </summary>
        /// <param name="slotId"> Id Of Slot</param>
        /// <returns> Return Success or Failed</returns>
        public JsonResult DeleteTimeSlot(long slotId)
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                SlotId = slotId
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteTimeSlotDetails", "AdminPanel", slotViewModel);
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
        /// Update Slot Detail PopUp
        /// </summary>
        /// <param name="slotId">Id Of Slot</param>
        /// <param name="timeSpan">Time Of Slot</param>
        /// <returns>Return Update Popup Details In String Format</returns>
        public JsonResult UpdatePopUp(long slotId, TimeSpan timeSpan)
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                SlotId = slotId,
                Time = timeSpan
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TimeSlotDetail/_Update.cshtml", slotViewModel);

            return Json(new { code = 0, message = convertedData });

        }

        /// <summary>
        /// Delete PopUp For Time Slot
        /// </summary>
        /// <param name="slotId"> Id Of Slot</param>
        /// <returns> Return Details Of PopUp In String Format</returns>
        public JsonResult DeletePopUp(long slotId)
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                SlotId = slotId
                
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TimeSlotDetail/_Delete.cshtml", slotViewModel);

            return Json(new { code = 0, message = convertedData });

        }
    }
}