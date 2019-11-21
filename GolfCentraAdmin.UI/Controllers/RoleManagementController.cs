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
    public class RoleManagementController : MyBaseController
    {
        // GET: RoleManagement
        public ActionResult Index()
        {
            TempData["EmployeeList"] = null;
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllActiveEmployeeForList", "AdminPanel", new EmployeeViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EmployeeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.EmployeeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    TempData["EmployeeList"] = dbBookingDetails.Content;
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
        /// Get All Permission Details By Employee Id
        /// </summary>
        /// <param name="employeeId">Id Of Employee</param>
        /// <returns> Return Permission Details In String</returns>
        public JsonResult GetPermissionInformationByEmployeeId(long employeeId)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = employeeId
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllPermissionDetailsForEmployee", "AdminPanel", employeeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.PermissionMainViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.PermissionMainViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/RoleManagement/_RoleDetails.cshtml", dbBookingDetails.Content);

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
        /// Update Or Save Pages Permission For User
        /// </summary>
        /// <param name="permissionMainViewModel">Model Of Type PermissionMainViewModel</param>
        /// <returns> Return Success Or Failed</returns>
        public JsonResult SaveRoleManagement(PermissionMainViewModel permissionMainViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdatePermission", "AdminPanel", permissionMainViewModel);
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