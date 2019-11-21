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
    public class PromotionController : MyBaseController
    {
        // GET: Promotion
        public ActionResult Index()
        {
            PromotionViewModel promotionViewModel = new PromotionViewModel() {
                TaxManagementViewModels= GetAllTaxs(),
                EquipmentViewModel= GetAllEquipment()
            };
            return View(promotionViewModel);
        }

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

        public List<ViewModel.EquipmentViewModel> GetAllEquipment()
        {

            HttpResponseMessage response2 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllEquipmentType", "AdminPanel", new EquipmentViewModel());
            if (response2.IsSuccessStatusCode)
            {
                var data = response2.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EquipmentViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    return  dbBookingDetails.Content;
                }
                else
                {
                    return new List<EquipmentViewModel>();
                }
            }
            else
            {
                return new List<EquipmentViewModel>();
            }

        }


        [ValidateAntiForgeryToken]
        public JsonResult SavePromotion(PromotionViewModel promotionViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SavePromotion", "AdminPanel", promotionViewModel);
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


        public ActionResult GetAllPromotion()
        {

            HttpResponseMessage response2 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllPromotion", "AdminPanel", new PromotionViewModel());
            if (response2.IsSuccessStatusCode)
            {
                var data = response2.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PromotionViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PromotionViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    return View(dbBookingDetails.Content);
                }
                else
                {
                    return View(new List<PromotionViewModel>());
                }
            }
            else
            {
                return View(new List<PromotionViewModel>());
            }

        }

        public JsonResult DeletePromotion(long id)
        {
            PromotionViewModel promotionViewModel = new PromotionViewModel()
            {
                PromotionsId = id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeletePromotion", "AdminPanel", promotionViewModel);
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