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
    public class UpdateSessionPriceController : MyBaseController
    {
        // GET: UpdateSessionPrice
        public ActionResult Index(long id)
        {
            TempData["slotViewList"] = null;
            SlotViewModel slotViewModel = new SlotViewModel() { SessionId=id};
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllActiveSlotBySessionId", "AdminPanel", slotViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["slotViewList"] = dbBookingDetails.Content;
                }
                else
                {
                    //  return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                // return Json(new { code = -2, message = "failed" });
            }

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
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SavePricing", "AdminPanel", updatePriceViewModel);
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