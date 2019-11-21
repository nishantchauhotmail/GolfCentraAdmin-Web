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
    public class AddSpecialPricingController : MyBaseController
    {
        // GET: AddSpecialPricing
        public ActionResult Index()
        {

            PricingViewModel pricingViewModel = new PricingViewModel()
            {
                SlotViewModels = GetAllSlotTime(),
                HoleTypeViewModels = GetAllHoleType(),
                MemberTypeViewModels = GetAllMemberType(),
                BookingTypeViewModels = GetAllBookingType(),
                TaxManagementViewModels = GetAllTaxs()

            };
            return View(pricingViewModel);
        }
        /// <summary>
        /// Get All Active Time Slot Detail
        /// </summary>
        /// <returns></returns>

        private List<SlotViewModel> GetAllSlotTime()
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllTimeSlot", "AdminPanel", slotViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<SlotViewModel>();
                }
            }
            else
            {
                return new List<SlotViewModel>();
            }

        }
        /// <summary>
        /// Get Booking Details
        /// </summary>
        /// <returns></returns>

        private List<BookingTypeViewModel> GetAllBookingType()
        {
            BookingTypeViewModel bookTypeViewModel = new BookingTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllBookingType", "AdminPanel", bookTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BookingTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<BookingTypeViewModel>();
                }
            }
            else
            {
                return new List<BookingTypeViewModel>();
            }

        }

        /// <summary>
        /// Get All Member Type Details
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
        /// Get All Hole Information
        /// </summary>
        /// <returns></returns>

        private List<HoleTypeViewModel> GetAllHoleType()
        {
            HoleTypeViewModel holeTypeViewModel = new HoleTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllHoleType", "AdminPanel", holeTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.HoleTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.HoleTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<HoleTypeViewModel>();
                }
            }
            else
            {
                return new List<HoleTypeViewModel>();
            }

        }
        /// <summary>
        /// Get All Tax Type Detail
        /// </summary>
        /// <returns></returns>

        private List<TaxManagementViewModel> GetAllTaxs()
        {
            TaxManagementViewModel taxViewModel = new TaxManagementViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllTaxType", "AdminPanel", taxViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.TaxManagementViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.TaxManagementViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<TaxManagementViewModel>();
                }
            }
            else
            {
                return new List<TaxManagementViewModel>();
            }

        }
        /// <summary>
        /// Get All Pricing Details
        /// </summary>
        /// <param name="pricingViewModel"></param>
        /// <returns></returns>

        public JsonResult GetPricingDetails(PricingViewModel pricingViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetPriceDetail", "AdminPanel", pricingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.PricingViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.PricingViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    return Json(new { code = 0, message = dbBookingDetails.Content });
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
        /// Save Pricing Detail
        /// </summary>
        /// <param name="pricingViewModel"></param>
        /// <returns></returns>

        public JsonResult SavePricing(PricingViewModel pricingViewModel)
        {
            pricingViewModel.IsSpecialPricing = true;
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SavePricingNew", "AdminPanel", pricingViewModel);
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