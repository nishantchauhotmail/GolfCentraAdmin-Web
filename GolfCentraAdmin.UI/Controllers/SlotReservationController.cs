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
    public class SlotReservationController : MyBaseController
    {
        // GET: SlotReservation
        public ActionResult Index() 
        {
            BlockSlotRangeViewModel blockSlotRangeViewModel = new BlockSlotRangeViewModel() {
                SlotBlockReasonViewModels = GetAllSlotBlockReason(),
                CoursePairingViewModels = GetAllCourseName()
            };
            return View(blockSlotRangeViewModel);
        }


        public List<ViewModel.SlotBlockReasonViewModel> GetAllSlotBlockReason()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetALLSlotBlockReason", "AdminPanel", new SlotViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotBlockReasonViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SlotBlockReasonViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<SlotBlockReasonViewModel>();
                }
            }
            else
            {
                return new List<SlotBlockReasonViewModel>();
            }
        }
        public List<ViewModel.CoursePairingViewModel> GetAllCourseName()
        {
            List<CoursePairingViewModel> coursePairingViewModel = new List<CoursePairingViewModel>
            {
                new CoursePairingViewModel() { CoursePairingId = 999, Value = "All" },
            
                  new CoursePairingViewModel() { CoursePairingId = 4, Value = "Ridge" },
                new CoursePairingViewModel() { CoursePairingId = 5, Value = "Valley" },
                new CoursePairingViewModel() { CoursePairingId = 6, Value = "Canyon" }
            };
            return coursePairingViewModel;
        }

        public JsonResult GetAllBlockSlotRange()
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetSlotBlockRangeDetails", "AdminPanel", new SlotViewModel());
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BlockSlotRangeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BlockSlotRangeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SlotReservation/_BlockSlotRangeDetail.cshtml", dbBookingDetails.Content);

                    return Json(new { code = 0, message = convertedData });
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

     
        public JsonResult DeletePopUp(long blockSlotRangeId)
        {
            BlockSlotRangeViewModel slotViewModel = new BlockSlotRangeViewModel()
            {
                BlockSlotRangeId = blockSlotRangeId

            };
            string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/SlotReservation/_Delete.cshtml", slotViewModel);

            return Json(new { code = 0, message = convertedData });

        }

     
        public JsonResult DeleteBlockSlotRange(long blockSlotRangeId)
        {
            BlockSlotRangeViewModel slotViewModel = new BlockSlotRangeViewModel()
            {
                BlockSlotRangeId = blockSlotRangeId
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("DeleteBlockSlotRangeDetails", "AdminPanel", slotViewModel);
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


        public JsonResult GetALLSlotForBlockRange(DateTime startDate,DateTime endDate)
        {
            BlockSlotRangeViewModel slotViewModel = new BlockSlotRangeViewModel()
            {
               StartDate =startDate,
                EndDate =endDate
            };
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetALLSlotForBlockRange", "AdminPanel", slotViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BlockSlotViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.BlockSlotViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return Json(new { code = 0, message = dbBookingDetails.Content });
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

        [ValidateAntiForgeryToken]
        public JsonResult SaveBlockSlotRangeDetail(BlockSlotRangeViewModel blockSlotRangeViewModel)
        {       
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveBlockSlotRangeDetail", "AdminPanel", blockSlotRangeViewModel);
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

    }
}
