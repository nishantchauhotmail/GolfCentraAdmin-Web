using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;

namespace GolfCentraAdmin.UI.Controllers
{
    public class BucketDetailController : MyBaseController
    {
        // GET: BucketDetail
        public ActionResult Index()
        {
            BucketViewModel bucketViewModel = new BucketViewModel()
            {

                TaxManagementViewModels = GetAllTaxs(),
                MemberTypeViewModels=GetAllMemberType(),
                DayTypeViewModels=GetAllDayType()
            };
            return View(bucketViewModel);
        }
        /// <summary>
        /// Get All Day Type Details
        /// </summary>
        /// <returns></returns>

        private List<DayTypeViewModel> GetAllDayType()
        {
            DayTypeViewModel dayTypeViewModel = new DayTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllDayType", "AdminPanel", dayTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.DayTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.DayTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<DayTypeViewModel>();
                }
            }
            else
            {
                return new List<DayTypeViewModel>();
            }

        }
        /// <summary>
        /// Get All Member Type Detail
        /// </summary>
        /// <returns></returns>

        private List<MemberTypeViewModel> GetAllMemberType()
        {
            BucketViewModel memberTypeViewModel = new BucketViewModel()
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
        /// Get All Bucket Detail
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllBucketType()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllBucketType", "AdminPanel", new BucketViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BucketViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BucketViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BucketDetail/_BucketDetail.cshtml", dbBookingDetails.Content);

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
        /// Delete Bucket Type Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteBucketType(long id)
        {
            BucketViewModel bucketViewModel = new BucketViewModel()
            {
                BucketDetailId = id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteBucketDetails", "AdminPanel", bucketViewModel);
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
        /// Delete Pop up For Bucket Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeletePopUp(long id)
        {
            BucketViewModel bucketViewModel = new BucketViewModel()
            {
                BucketDetailId = id

            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BucketDetail/_Delete.cshtml", bucketViewModel);

            return Json(new { code = 0, message = convertedData });

        }
        /// <summary>
        /// Pop Up for Tax Details
        /// </summary>
        /// <param name="bucketTaxMappingViewModels"></param>
        /// <returns></returns>

        public JsonResult TaxDetailsPopUp(List<BucketTaxMappingViewModel> bucketTaxMappingViewModels)
        {

            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BuckettDetail/_TaxMappingDetail.cshtml", bucketTaxMappingViewModels);

            return Json(new { code = 0, message = convertedData });
        }
        /// <summary>
        /// Save Bucket Type Detail
        /// </summary>
        /// <param name="bucketViewModel"></param>
        /// <returns></returns>
        public JsonResult SaveBucketType(BucketViewModel bucketViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveBucketDetails", "AdminPanel", bucketViewModel);
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
        /// Update Bucket Type Detail
        /// </summary>
        /// <param name="bucketViewModel"></param>
        /// <returns></returns>
        public JsonResult UpdateBucketType(BucketViewModel bucketViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateBucketDetails", "AdminPanel", bucketViewModel);
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
        /// Update Pop Up For Bucket Type
        /// </summary>
        /// <param name="bucketViewModel"></param>
        /// <returns></returns>
        public JsonResult UpdatePopUp(BucketViewModel bucketViewModel)
        {
            bucketViewModel.TaxManagementViewModels = GetAllTaxs();
            bucketViewModel.MemberTypeViewModels = GetAllMemberType();
            bucketViewModel.DayTypeViewModels = GetAllDayType();
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/BucketDetail/_Update.cshtml", bucketViewModel);

            return Json(new { code = 0, message = convertedData });

        }
    }
}