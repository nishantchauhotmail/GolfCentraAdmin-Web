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
    public class ReportController : MyBaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        /// <summary>
        /// Search Booking By advance Search
        /// </summary>
        /// <param name="bookingViewModel">Model Of Type Booking View Model</param>
        /// <returns> Return Details In String Format</returns>
        public JsonResult SearchByAdvanceSearch(BookingViewModel bookingViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBookingDetailsBySearch", "AdminPanel", bookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/Report/_Search.cshtml", dbBookingDetails.Content);

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
        /// Get Booking Details By Booking Id
        /// </summary>
        /// <param name="bookingId">Id Of Booking</param>
        /// <returns>Return Booking Details By Booking Id</returns>
        public JsonResult GetBookingByBookingId(long bookingId)
        {
            BookingViewModel bookingViewModel = new BookingViewModel()
            {
                BookingId = bookingId
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBookingDetailByBookingId", "AdminPanel", bookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.BookingViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.BookingViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/Report/_BookingDetailsPopUp.cshtml", dbBookingDetails.Content);

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