using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;

namespace GolfCentraAdmin.UI.Controllers
{
    public class MemberTypeDetailController : MyBaseController
    {
        // GET: MemberTypeDetail
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get All Member Type Detail
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMemberType()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMemberType", "AdminPanel", new MemberTypeViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/MemberTypeDetail/_MemberTypeDetail.cshtml", dbBookingDetails.Content);

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
        /// Save Member Type Details
        /// </summary>
        /// <param name="timeSlot"></param>
        /// <returns></returns>
        public JsonResult SaveMemberType(string memberTypeName, string memberTypeValue)
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {
                Name=memberTypeName,
                ValueToShow=memberTypeValue
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveMemberTypeDetails", "AdminPanel", memberTypeViewModel);
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
        /// Update MemberType Details
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="timeSlot"></param>
        /// <returns></returns>
        public JsonResult UpdateMemberType(long id, String memberType,string valueToShow)
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {
                Name= memberType,
                MemberTypeId = id,
                ValueToShow=valueToShow
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateMemberTypeDetails", "AdminPanel", memberTypeViewModel
);
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
        /// Delete Member Type
        /// </summary>
        /// <param name="slotId"></param>
        /// <returns></returns>
        public JsonResult DeleteMemberType(long id)
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {
                MemberTypeId=id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteMemberTypeDetails", "AdminPanel", memberTypeViewModel);
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
        /// Update Pop up for Memebr Type
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public JsonResult UpdatePopUp(long id, string memberType,string valueToShow)
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {
                MemberTypeId= id,
                Name=memberType,
                ValueToShow=valueToShow
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/MemberTypeDetail/_Update.cshtml", memberTypeViewModel);

            return Json(new { code = 0, message = convertedData });

        }
        /// <summary>
        /// Delete pop up for Member Type
        /// </summary>
        /// <param name="slotId"></param>
        /// <returns></returns>
        public JsonResult DeletePopUp(long id)
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {
                MemberTypeId= id

            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/MemberTypeDetail/_Delete.cshtml", memberTypeViewModel);

            return Json(new { code = 0, message = convertedData });

        }



    }
}