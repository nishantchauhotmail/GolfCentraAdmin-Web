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
    public class OnSpotBookingController : MyBaseController
    {
        // GET: OnSpotBooking
        public ActionResult Index(long? sessionSlotId,long? coursePairingId,long? sessionId)
        {
            SaveBookingViewModel saveBookingViewModel = new SaveBookingViewModel()
            {
                SlotSessionId = sessionSlotId,
                CoursePairingId=coursePairingId,
                SessionId=sessionId
            };
            SessionMasterViewModel sessionMasterViewModel = new SessionMasterViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllSessionDetail", "AdminPanel", sessionMasterViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["SessionList"] = dbBookingDetails.Content.Where(x=>x.BookingTypeId==1).ToList();
                    TempData["SessionListBDR"] = dbBookingDetails.Content.Where(x => x.BookingTypeId == 2).ToList();

                    //  return Json(new { code = 0, message = dbBookingDetails.Content });
                }
                else
                {
                   // return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
              //  return Json(new { code = -2, message = "failed" });
            }

            HttpResponseMessage response1 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBucketDetailList", "AdminPanel", new BucketViewModel() );
            if (response1.IsSuccessStatusCode)
            {
                var data = response1.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BucketViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BucketViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["BucketList"] = dbBookingDetails.Content;
                    //  return Json(new { code = 0, message = dbBookingDetails.Content });
                }
                else
                {
                    // return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                //  return Json(new { code = -2, message = "failed" });
            }

            HttpResponseMessage response2 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMemberType", "AdminPanel", new MemberTypeViewModel());
            if (response2.IsSuccessStatusCode)
            {
                var data = response2.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["MemberTypeList"] = dbBookingDetails.Content;
                    //  return Json(new { code = 0, message = dbBookingDetails.Content });
                }
                else
                {
                    // return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                //  return Json(new { code = -2, message = "failed" });
            }

            HttpResponseMessage response3 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllEquipmentType", "AdminPanel", new EquipmentViewModel());
            if (response3.IsSuccessStatusCode)
            {
                var data = response3.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["EquipmentList"] = dbBookingDetails.Content;
                    //  return Json(new { code = 0, message = dbBookingDetails.Content });
                }
                else
                {
                    // return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                //  return Json(new { code = -2, message = "failed" });
            }

            return View(saveBookingViewModel);
        }
        /// <summary>
        /// Get All Session Detail
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>

        public JsonResult GetAllSessionDetail(long employeeId)
        {
            SessionMasterViewModel sessionMasterViewModel = new SessionMasterViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllSessionDetail", "AdminPanel", sessionMasterViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SessionMasterViewModel>>>(data);
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
        /// Get All Slot Detail
        /// </summary>
        /// <param name="bookingTypeId"></param>
        /// <param name="date"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>

        public JsonResult GetSlotDetails(long bookingTypeId, DateTime date, long sessionId,long? coursePairingId)
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                SessionId = sessionId,
                Date = date,
                BookingTypeId = bookingTypeId,
                CoursePairingId = coursePairingId

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllSlotDetailForSpotBooking", "AdminPanel", slotViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>>>(data);
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
        /// Get All Calculated price
        /// </summary>
        /// <param name="bookingPricingViewModel"></param>
        /// <returns></returns>

        public JsonResult GetPriceCalculation(BookingPricingViewModel bookingPricingViewModel)
        {
       
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBookingPricingCalcalution", "AdminPanel", bookingPricingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<BookingPricingViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<BookingPricingViewModel>>(data);
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
        /// Save Booking Detail
        /// </summary>
        /// <param name="saveBookingViewModel"></param>
        /// <returns></returns>

        public JsonResult SaveBooking(SaveBookingViewModel saveBookingViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveBooking", "AdminPanel", saveBookingViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<Dictionary<string, bool>>>(data);
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
        /// Get All Member Type Detail
        /// </summary>
        /// <returns></returns>

        public JsonResult GetMemberType()
        {

            HttpResponseMessage response2 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMemberType", "AdminPanel", new MemberTypeViewModel());
            if (response2.IsSuccessStatusCode)
            {
                var data = response2.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>>>(data);
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

        public JsonResult GetAllBucketList(long memberTypeId,DateTime date)
        {

            HttpResponseMessage response1 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetBucketDetailList", "AdminPanel", new BucketViewModel() { MemberTypeId=memberTypeId,Date=date});
            if (response1.IsSuccessStatusCode)
            {
                var data = response1.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BucketViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BucketViewModel>>>(data);
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



        ///Get Equipment Details

        public JsonResult GetEquipmentDetails()
        {
           
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllEquipmentType", "AdminPanel", new EquipmentViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>>>(data);
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



        public JsonResult GetAllEquipment()
        {

            HttpResponseMessage response2 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllEquipmentType", "AdminPanel", new EquipmentViewModel());
            if (response2.IsSuccessStatusCode)
            {
                var data = response2.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>>>(data);
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


        public JsonResult SearchGolferByMemberShipId(string memberShipId)
        {

            HttpResponseMessage response1 = new APIHelper.APICallMethod().GetHttpResponseMessage("SearchGolferByMemberShipId", "AdminPanel", new GolferViewModel() { ClubMemberId = memberShipId });
            if (response1.IsSuccessStatusCode)
            {
                var data = response1.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GolferViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GolferViewModel>>>(data);
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


        public JsonResult GetCouponByCode(string code)
        {

            HttpResponseMessage response1 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetCouponByCode", "AdminPanel", new CouponViewModel() { Code = code });
            if (response1.IsSuccessStatusCode)
            {
                var data = response1.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.CouponViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.CouponViewModel>>(data);
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

    }
}