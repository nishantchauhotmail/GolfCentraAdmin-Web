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
    public class EquipmentDetailController : MyBaseController
    {
        // GET: EquipmentDetail
        public ActionResult Index()
        {
             EquipmentViewModel equipmentViewModel = new EquipmentViewModel()
            {
                
                TaxManagementViewModels = GetAllTaxs()

            };
            return View(equipmentViewModel);
        }
        //Get All Tax Type Detail
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
                    TempData["taxList"] = dbBookingDetails.Content;
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
        /// Get All Equipment Type Detail
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllEquipmentType()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllEquipmentType", "AdminPanel", new EquipmentViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EquipmentDetail/_EquipmentDetail.cshtml", dbBookingDetails.Content);

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
        ///  Delete Equipment Type Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public JsonResult DeleteEquipmentType(long id)
        {
            EquipmentViewModel equipmentViewModel = new EquipmentViewModel()
            {
                EquipmentId= id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteEquipmentDetails", "AdminPanel", equipmentViewModel);
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
        /// Delete Pop Up For Equipment Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeletePopUp(long id)
        {
            EquipmentViewModel equipmentViewModel = new EquipmentViewModel()
            {
                EquipmentId = id

            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EquipmentDetail/_Delete.cshtml", equipmentViewModel);

            return Json(new { code = 0, message = convertedData });

        }
        public JsonResult TaxDetailsPopUp(List<EquipmentTaxMappingViewModel> equipmentTaxMappingViewModels)
        {
       
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EquipmentDetail/_TaxMappingDetail.cshtml", equipmentTaxMappingViewModels);

            return Json(new { code = 0, message = convertedData });
        }
        /// <summary>
        /// Save Equipment Type Detail
        /// </summary>
        /// <param name="equipmentViewModel"></param>
        /// <returns></returns>
        public JsonResult SaveEquipmentType(EquipmentViewModel equipmentViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveEquipmentDetails", "AdminPanel", equipmentViewModel);
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
        /// Update Equipment Type Detail
        /// </summary>
        /// <param name="equipmentViewModel"></param>
        /// <returns></returns>
        public JsonResult UpdateEquipmentType(EquipmentViewModel equipmentViewModel)
        {
       
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateEquipmentDetails", "AdminPanel",equipmentViewModel);
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
        /// Update Pop Up For Equipment Type
        /// </summary>
        /// <param name="equipmentViewModel"></param>
        /// <returns></returns>
        public JsonResult UpdatePopUp(EquipmentViewModel equipmentViewModel)
        {
            equipmentViewModel.TaxManagementViewModels = GetAllTaxs();
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EquipmentDetail/_Update.cshtml", equipmentViewModel);

            return Json(new { code = 0, message = convertedData });

        }
    }
}