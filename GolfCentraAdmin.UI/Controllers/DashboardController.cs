using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static GolfCentraAdmin.UI.Common.MyAttribute;

namespace GolfCentraAdmin.UI.Controllers
{
    public class DashboardController : MyBaseController
    {
        // GET: Dashboard
      
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Recent Booking By No Of Records
        /// </summary>
        /// <param name="noOfRecord">Count Of Record</param>
        /// <returns>Reteurn List Of Booking Details</returns>
        public JsonResult GetRecentBookingByTake(int noOfRecord)
        {
            BookingViewModel bookingViewModel = new BookingViewModel()
            {
                NoOfRecord = noOfRecord
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetRecentBookingByTake", "AdminPanel", bookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/Dashboard/_RecentBooking.cshtml", dbBookingDetails.Content);

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
        /// <param name="bookingId">Id Of Booking</param>
        /// <returns> Return Booking Details</returns>
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
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/Dashboard/_BookingDetailsPopUp.cshtml", dbBookingDetails.Content);

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
        /// Get Dashboard Top Row Data
        /// </summary>
        /// <returns>Return Top Row Data</returns>
        public JsonResult GetDataForDashboardTopBar()
        {
            BookingViewModel bookingViewModel = new BookingViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetDataForDashboardTopBar", "AdminPanel", bookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.DashBoardTopBarViewModel> dbDBTBViewModel = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.DashBoardTopBarViewModel>>(data);
                if (dbDBTBViewModel.IdentityResult.Status == true)
                {

                    return Json(new { code = 0, message = dbDBTBViewModel.Content });
                }
                else
                {
                    if (dbDBTBViewModel.IdentityResult.Message == "Invalid access details, Please log-out.")
                    {
                        return Json(new { code = -99, message = "Please Login Again" });
                    }
                    else
                    {
                        return Json(new { code = -1, message = dbDBTBViewModel.IdentityResult.Message });
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

