using GolfCentraAdmin.UI.Common;
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
    public class ChangePasswordController : MyBaseController
    {
        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UpdatePassword(EmployeeViewModel employeeViewModel)
        {

            ViewModel.EmployeeViewModel session = (ViewModel.EmployeeViewModel)HttpContext.Session[Constants.SessionName];
            employeeViewModel.EmployeeId = session.EmployeeId;
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateEmployeePassword", "AdminPanel", employeeViewModel);
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