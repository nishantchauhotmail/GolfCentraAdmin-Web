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
    public class DetailReportingController : MyBaseController
    {
        // GET: DetailReporting
        public ActionResult Index()
        {
            ReportingViewModel reportingViewModel = new ReportingViewModel() {
                BookingStatusViewModels = GetAllBookingStatus(),
                SessionMasterViewModels= GetAllSessionDetail(),
                MemberTypeViewModels= GetAllMemberType()
            
            };
            return View(reportingViewModel);
        }
        [ValidateAntiForgeryToken]
        public JsonResult BookingReport(ReportingViewModel reportingViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetReportBookingDetailsBySearch", "AdminPanel", reportingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ReportingViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ReportingViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/DetailReporting/_BookingReport.cshtml", dbBookingDetails.Content);

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
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/DetailReporting/_BookingDetailsPopUp.cshtml", dbBookingDetails.Content);

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


        public ActionResult Money()
        {
            ReportingViewModel reportingViewModel = new ReportingViewModel()
            {
                BookingStatusViewModels = GetAllBookingStatus(),
                PromotionViewModels = GetAllPromotion(),
                MemberTypeViewModels = GetAllMemberType()

            };
            return View(reportingViewModel);
        }


        public List<ViewModel.MemberTypeViewModel> GetAllMemberType()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMemberType", "AdminPanel", new MemberTypeViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<MemberTypeViewModel>();
                }
            }
            else
            {
                return new List<MemberTypeViewModel>();
            }

        }



        public List<ViewModel.SessionMasterViewModel> GetAllSessionDetail()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllSessionDetail", "AdminPanel", new SessionMasterViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    List<ViewModel.SessionMasterViewModel> sessionMasterViewModels = new List<SessionMasterViewModel>();
                    foreach(var item in dbBookingDetails.Content)
                    {
                        SessionMasterViewModel sessionMasterViewModel = new SessionMasterViewModel()
                        {
                            SessionId= item.SessionId,
                            SessionName= item.SessionName +" ("+ item.BookingTypeName +")"
                        };
                        sessionMasterViewModels.Add(sessionMasterViewModel);

                    }
                    return sessionMasterViewModels;
                }
                else
                {
                    return new List<SessionMasterViewModel>();
                }
            }
            else
            {
                return new List<SessionMasterViewModel>();
            }

        }


        public List<ViewModel.BookingStatusViewModel> GetAllBookingStatus()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllBookingStatus", "AdminPanel", new SessionMasterViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingStatusViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingStatusViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<BookingStatusViewModel>();
                }
            }
            else
            {
                return new List<BookingStatusViewModel>();
            }

        }


        public List<ViewModel.PromotionViewModel> GetAllPromotion()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllPromotion", "AdminPanel", new SessionMasterViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PromotionViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PromotionViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<PromotionViewModel>();
                }
            }
            else
            {
                return new List<PromotionViewModel>();
            }

        }

        [ValidateAntiForgeryToken]
        public JsonResult MoneyReport(ReportingViewModel reportingViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetMoneyDetailsBySearch", "AdminPanel", reportingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ReportingViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.ReportingViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/DetailReporting/_MoneyReport.cshtml", dbBookingDetails.Content);

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