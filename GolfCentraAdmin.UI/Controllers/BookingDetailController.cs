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
    public class BookingDetailController : MyBaseController
    {
        // GET: BookingDetail
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Get Booking Details
        /// </summary>
        /// <returns>Return Booking Details</returns>
        public JsonResult GetBookingDetails()
        {
            BookingViewModel bookingViewModel = new BookingViewModel()
            {
               
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBookingDetails", "AdminPanel", bookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BookingDetail/_Search.cshtml", dbBookingDetails.Content);

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
        /// Get Booking By Booking Id
        /// </summary>
        /// <param name="bookingId">booking Id of Type Long</param>
        /// <returns>Return Booking Details</returns>
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
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BookingDetail/_BookingDetailsPopUp.cshtml", dbBookingDetails.Content);

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


        public JsonResult CancelBooking(BookingViewModel bookingViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("CancelBookingByBookingId", "AdminPanel", bookingViewModel);
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

        public JsonResult CancelBookingPopUP(long bookingId)
        {
            BookingViewModel bookingViewModel = new BookingViewModel()
            {
                BookingId=bookingId
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BookingDetail/_CancelBookingPopUp.cshtml", bookingViewModel);

            return Json(new { code = 0, message = convertedData });

        }
    }
}