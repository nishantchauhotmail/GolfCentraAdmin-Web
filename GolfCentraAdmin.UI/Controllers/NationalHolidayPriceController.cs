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
    public class NationalHolidayPriceController : MyBaseController
    {
        // GET: NationalHolidayPrice
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Save Pricing Detail
        /// </summary>
        /// <param name="updatePriceViewModel"></param>
        /// <returns></returns>

        public JsonResult SavePricing(UpdatePriceViewModel updatePriceViewModel)
        {

            if (updatePriceViewModel.Member == true) { updatePriceViewModel.MemberTypeId = 1; }
            else { updatePriceViewModel.MemberTypeId = 2; }
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveNationalHolidayPricing", "AdminPanel", updatePriceViewModel);
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
                    return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                return Json(new { code = -2, message = "failed" });
            }

        }

    }
}