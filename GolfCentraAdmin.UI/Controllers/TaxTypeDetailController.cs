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
    public class TaxTypeDetailController : MyBaseController
    {
        // GET: TaxTypeDetail
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get All Tax Type Detail
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllTaxType()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllTaxType", "AdminPanel", new TaxManagementViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.TaxManagementViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.TaxManagementViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TaxTypeDetail/_TaxTypeDetail.cshtml", dbBookingDetails.Content);

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
        /// Save Tax Type Detail
        /// </summary>
        /// <param name="memberTypeName"></param>
        /// <param name="memberTypeValue"></param>
        /// <returns></returns>
        public JsonResult SaveTaxType(string taxTypeName, decimal percentage)
        {
            TaxManagementViewModel taxManagementViewModel = new TaxManagementViewModel()
            {
                Name = taxTypeName,
                Percentage = percentage
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveTaxTypeDetails", "AdminPanel", taxManagementViewModel);
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
        /// Update Tax Type Detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberType"></param>
        /// <param name="valueToShow"></param>
        /// <returns></returns>
        public JsonResult UpdateTaxType(long id, string taxTypeName, decimal percentage)
        {
            TaxManagementViewModel taxManagementViewModel = new TaxManagementViewModel()
            {
                Name = taxTypeName,
                TaxId = id,
                Percentage = percentage
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateTaxTypeDetails", "AdminPanel", taxManagementViewModel
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
        /// Delete Tax Type Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteTaxType(long id)
        {
            TaxManagementViewModel taxManagementViewModel = new TaxManagementViewModel()
            {
                TaxId = id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteTaxTypeDetails", "AdminPanel", taxManagementViewModel);
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
        /// Update Pop Up For Tax Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberType"></param>
        /// <param name="valueToShow"></param>
        /// <returns></returns>
        public JsonResult UpdatePopUp(long id, string taxTypeName, decimal percentage)
        {
            TaxManagementViewModel taxManagementViewModel = new TaxManagementViewModel()
            {
                TaxId= id,
                Name = taxTypeName,
                Percentage = percentage
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TaxTypeDetail/_Update.cshtml", taxManagementViewModel);

            return Json(new { code = 0, message = convertedData });

        }
        /// <summary>
        /// Delete Pop Up For Tax Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeletePopUp(long id)
        {
            TaxManagementViewModel taxManagementViewModel = new TaxManagementViewModel()
            {
                TaxId = id

            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/TaxTypeDetail/_Delete.cshtml", taxManagementViewModel);

            return Json(new { code = 0, message = convertedData });

        }
    }

}