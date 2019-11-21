using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;

namespace GolfCentraAdmin.UI.Controllers
{
    public class SearchGolferController : MyBaseController
    {
        // GET: SearchGolfer
        public ActionResult Index()

        {
            GolferViewModel golferViewModel = new GolferViewModel()
            {
                MemberTypeViewModels = GetAllMemberType(),
                GenderTypeViewModels = GetAllGenderType(),
                MaritalStatusViewModels = GetAllMaritalStatusType()
            };

            return View(golferViewModel);
        }

        [ValidateAntiForgeryToken]
        /// <summary>
        /// Get Golfer By Advance Search
        /// </summary>
        /// <param name="golferViewModel"></param>
        /// <returns></returns>
        public JsonResult GetGolferByAdvanceSearch(GolferViewModel golferViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetGolferByAdvanceSearch", "AdminPanel", golferViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<GolfCentraAdmin.ViewModel.GolferViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<GolfCentraAdmin.ViewModel.GolferViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SearchGolfer/_SearchGolfer.cshtml", dbBookingDetails.Content);
                    return Json(new { code = 0, message = convertedData });
                }
                else
                {
                    return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                return Json(new { code = -2, message = "failed" });
            }

        }
        /// <summary>
        /// Get All Member Type Detail
        /// </summary>
        /// <returns></returns>
        private List<MemberTypeViewModel> GetAllMemberType()
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMemberType", "AdminPanel", memberTypeViewModel);
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
        /// <summary>
        /// Get All Gender Type Detail
        /// </summary>
        /// <returns></returns>
        private List<GenderTypeViewModel> GetAllGenderType()
        {
            GenderTypeViewModel genderTypeViewModel = new GenderTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllGenderType", "AdminPanel", genderTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GenderTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GenderTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<GenderTypeViewModel>();
                }
            }
            else
            {
                return new List<GenderTypeViewModel>();
            }

        }
        /// <summary>
        /// Get All Marital Status Detail
        /// </summary>
        /// <returns></returns>
        private List<MaritalStatusViewModel> GetAllMaritalStatusType()
        {
            MaritalStatusViewModel maritalStatusViewModel = new MaritalStatusViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMaritalStatusType", "AdminPanel", maritalStatusViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MaritalStatusViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MaritalStatusViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<MaritalStatusViewModel>();
                }
            }
            else
            {
                return new List<MaritalStatusViewModel>();
            }

        }
        /// <summary>
        /// Get Golfer Detail By Golfer Id
        /// </summary>
        /// <param name="golferId"></param>
        /// <returns></returns>
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
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SearchGolfer/_GolferDetails.cshtml", dbBookingDetails.Content);

                    return Json(new { code = 0, message = convertedData });
                }
                else
                {
                    return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                return Json(new { code = -2, message = "failed" });
            }

        }
        /// <summary>
        /// Get User  Booking Detail By Golfer Id
        /// </summary>
        /// <param name="golferId"></param>
        /// <returns></returns>

        public JsonResult GetUserReportDetailByGolferId(long golferId)
        {
            BookingViewModel bookingViewModel = new BookingViewModel()
            {
                GolferId = golferId
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBookingDetailsByGolferId", "AdminPanel", bookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SearchGolfer/_GolferReportDetails.cshtml", dbBookingDetails.Content);

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

        public JsonResult UpdateGolferStatus(GolferViewModel golferViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("BlockUnBlockOperation", "AdminPanel", golferViewModel);
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

        public JsonResult UpdatePopUp(long golferId, bool status)
        {
            GolferViewModel golferViewModel = new GolferViewModel()
            {
                GolferId = golferId,
                IsBlocked = status
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SearchGolfer/_UpdateGolferStatus.cshtml", golferViewModel);

            return Json(new { code = 0, message = convertedData });

        }


        public JsonResult UpdateGolferPassword(GolferViewModel golferViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateGolferPassword", "AdminPanel", golferViewModel);
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

        public JsonResult UpdateGolferPasswordPopUp(long golferId)
        {
            GolferViewModel golferViewModel = new GolferViewModel()
            {
                GolferId = golferId,
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SearchGolfer/_ChangePassword.cshtml", golferViewModel);

            return Json(new { code = 0, message = convertedData });

        }

    }
}