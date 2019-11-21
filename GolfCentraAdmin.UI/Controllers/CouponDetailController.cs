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
    public class CouponDetailController : MyBaseController
    {
        // GET: CouponDetail
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        /// <summary>
        /// Save Coupon Details
        /// </summary>
        /// <param name="couponViewModel">Model Of Type CouponViewModel</param>
        /// <returns> Return Success OR Failed</returns>
        public JsonResult SaveCoupon(CouponViewModel couponViewModel)
        {
            couponViewModel.Status = true;
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveCouponDetail", "AdminPanel", couponViewModel);
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
        /// Save Coupon Details
        /// </summary>
        /// <param name="couponViewModel">Model Of Type CouponViewModel</param>
        /// <returns> Return Success OR Failed</returns>
        public JsonResult DeleteCoupon(long id)
        {
            CouponViewModel couponViewModel = new CouponViewModel()
            {
                CouponId = id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteCoupon", "AdminPanel", couponViewModel);
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

        public JsonResult GetAllActiveCoupon()
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllActiveCoupon", "AdminPanel", new CouponViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.CouponViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.CouponViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/CouponDetail/_couponDetail.cshtml", dbBookingDetails.Content);

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

        public JsonResult DeletePopUp(long id)
        {
            CouponViewModel couponViewModel = new CouponViewModel()
            {
                CouponId = id

            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/CouponDetail/_deleteCoupon.cshtml", couponViewModel);

            return Json(new { code = 0, message = convertedData });

        }

    }
}