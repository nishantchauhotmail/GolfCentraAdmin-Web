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
    public class EmployeeManagementController : MyBaseController
    {
        // GET: EmployeeManagement
        public ActionResult Index()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetALLGenderType", "AdminPanel", new GenderTypeViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GenderTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GenderTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    employeeViewModel.GenderTypeViewModels = dbBookingDetails.Content.ToList();
                }
                else
                {
                   // return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
             //   return Json(new { code = -2, message = "failed" });
            }

            HttpResponseMessage response1 = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllEmployeeType", "AdminPanel", new EmployeeTypeViewModel());
            if (response1.IsSuccessStatusCode)
            {
                var data = response1.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EmployeeTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EmployeeTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    employeeViewModel.EmployeeTypeViewModels = dbBookingDetails.Content.ToList();
                }
                else
                {
                    // return Json(new { code = -1, message = dbBookingDetails.IdentityResult.Message });
                }
            }
            else
            {
                //   return Json(new { code = -2, message = "failed" });
            }

            return View(employeeViewModel);
        }


        /// <summary>
        /// Get All Active Details
        /// </summary>
        /// <returns>Get All Active Details</returns>
        public JsonResult GetEmployeeManagementDetails()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
            
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllActiveEmployee", "AdminPanel", employeeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EmployeeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EmployeeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EmployeeManagement/_Search.cshtml", dbBookingDetails.Content);

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

        [ValidateAntiForgeryToken]
        /// <summary>
        /// Save Employee Detail
        /// </summary>
        /// <param name="employeeViewModel">Model Of Type EmployeeViewModel</param>
        /// <returns>Return Success Or Failed</returns>
        public JsonResult SaveEmployee(EmployeeViewModel employeeViewModel)
        {
            
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveEmployee", "AdminPanel", employeeViewModel);
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
        /// Update Employee Detail
        /// </summary>
        /// <param name="employeeViewModel">Model Of Type EmployeeViewModel</param>
        /// <returns>Return Success Or Failed</returns>
        public JsonResult UpdateEmployee(EmployeeViewModel employeeViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateEmployee", "AdminPanel", employeeViewModel);
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
        /// Update Employee Status
        /// </summary>
        /// <param name="employeeViewModel">Model Of Type EmployeeViewModel</param>
        /// <returns>Return Success Or Failed</returns>
        public JsonResult UpdateEmployeeStatus(EmployeeViewModel employeeViewModel)
        {

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateEmployeeStatus", "AdminPanel", employeeViewModel);
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
        /// Update Status PopUp
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="status">True Or False</param>
        /// <returns>Return String Data</returns>
        public JsonResult UpdatePopUp(long employeeId, bool status)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = employeeId,
                IsActive = status
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EmployeeManagement/_UpdateEmployeeStatus.cshtml", employeeViewModel);

            return Json(new { code = 0, message = convertedData });

        }

        /// <summary>
        /// Update Employee PopUp By Employee Id
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns>Return String Data</returns>
        public JsonResult UpdateEmployeePopUp(long employeeId)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = employeeId
            };

        
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetEmployeeProfileByEmployeeId", "AdminPanel", employeeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.EmployeeViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.EmployeeViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/EmployeeManagement/_UpdateEmployeeDetail.cshtml", dbBookingDetails.Content);

                    return Json(new { code = 0, message = convertedData });
                }
                else
                {
                    return Json(new { code = 0, message = "" });
                }
            }
            return Json(new { code = 0, message = "" });

            

        }
    }
}