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
    public class PaymentGatewayControlController : MyBaseController
    {
        // GET: PaymentGatewayControl
        public ActionResult Index()
        {
            PaymentGatewayControlViewModel paymentGatewayControlViewModel = new PaymentGatewayControlViewModel()
            {

                EquipmentViewModels = GetAllEquipment(),
                GolferViewModels= GetALLGolfer()
            };
            return View(paymentGatewayControlViewModel);
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

                    return dbBookingDetails.Content;
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
        public JsonResult SavePaymentGatewayControl(PaymentGatewayControlViewModel paymentGatewayControlViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SavePaymentGatewayControl", "AdminPanel", paymentGatewayControlViewModel);
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


        public ActionResult GetAllPaymentGatewayControl()
        {

            HttpResponseMessage response2 = new APIHelper.APICallMethod().GetHttpResponseMessage("SearchAllPaymentGatewayControl", "AdminPanel", new EquipmentViewModel());
            if (response2.IsSuccessStatusCode)
            {
                var data = response2.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PaymentGatewayControlViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PaymentGatewayControlViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    return View(dbBookingDetails.Content);
                }
                else
                {
                    return View(new List<PaymentGatewayControlViewModel>());
                }
            }
            else
            {
                return View(new List<PaymentGatewayControlViewModel>());
            }

        }


        public JsonResult DeletePaymentGatewayControl(long id)
        {
            PaymentGatewayControlViewModel promotionViewModel = new PaymentGatewayControlViewModel()
            {
                PaymentGatewayControlId = id
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeletePaymentGatewayControl", "AdminPanel", promotionViewModel);
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

        public List<GolferViewModel> GetALLGolfer()
        {
            List<GolferViewModel> golferViewModels = new List<GolferViewModel>();
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllGolferProfile", "AdminPanel", new GolferViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GolferViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GolferViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    golferViewModels = dbBookingDetails.Content;
                }
                else
                {
                    return golferViewModels;
                }
            }
            else
            {
                return golferViewModels;
            }
            return golferViewModels;
        }
    }
}
