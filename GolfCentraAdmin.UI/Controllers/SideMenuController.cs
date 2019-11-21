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
    public class SideMenuController : MyBaseController
    {
        // GET: SideMenu
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get All page Details
        /// </summary>
        /// <returns>Return All Page Details In String Format</returns>
        public JsonResult GetAllPageDetail()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllPageDetails", "AdminPanel", new PageViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PageViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.PageViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SideMenu/_SideMenuDetails.cshtml", dbBookingDetails.Content);

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
        /// Update Page Ordering
        /// </summary>
        /// <param name="id">Id Of Page</param>
        /// <param name="ordering">Order Number Of Page</param>
        /// <returns>Return Success Or Failed</returns>
        public JsonResult UpdatePageOrdering(long id,int ordering)
        {
            PageViewModel pageViewModel = new PageViewModel()
            {
                PageId=id,
                Ordering=ordering
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdatePageOrdering", "AdminPanel", pageViewModel);
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
        /// Update Page Ordering Popup
        /// </summary>
        /// <param name="id">Id Of Page</param>
        /// <param name="ordering">Order Number Of Page</param>
        /// <returns>Return Popup Details In String Format</returns>
        public JsonResult UpdatePopUp(long id, int ordering)
        {
            PageViewModel pageViewModel = new PageViewModel()
            {
                PageId = id,
                Ordering = ordering
            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SideMenu/_Update.cshtml", pageViewModel);

            return Json(new { code = 0, message = convertedData });

        }
    }
}