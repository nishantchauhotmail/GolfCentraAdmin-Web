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
    public class PasswordUpdateController : Controller
    {
        // GET: PasswordUpdate
        public ActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePassword(EmployeeViewModel employeeViewModel)
        {
            try
            {


                ViewModel.EmployeeViewModel session = (ViewModel.EmployeeViewModel)TempData["session"];
                TempData.Keep("session");
                employeeViewModel.EmployeeId = session.EmployeeId;

                HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateEmployeePassword", "AdminPanel", employeeViewModel, session.UniqueSessionId);
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
            catch (Exception)
            {

                return Json(new { code = -3, message = "failed" });
            }
        }
    }
}