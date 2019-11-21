using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;

namespace GolfCentraAdmin.UI.Controllers
{
    public class UpdateGolferController : MyBaseController
    {
        // GET: UpdateGolfer
        public ActionResult Index(long golferId)
        {
          

            GolferViewModel golferViewModel = new GolferViewModel()
            {
                GolferId = golferId
            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetGolferProfileByGolferId", "AdminPanel", golferViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<ViewModel.GolferViewModel> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<ViewModel.GolferViewModel>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {

                    dbBookingDetails.Content.MemberTypeViewModels = GetAllMemberType();
                    dbBookingDetails.Content.GenderTypeViewModels = GetAllGenderType();
                    dbBookingDetails.Content.MaritalStatusViewModels = GetAllMaritalStatusType();
                    dbBookingDetails.Content.SalutationViewModels = GetAllSalutationType();
                    return View(dbBookingDetails.Content);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

            
        }

        [ValidateAntiForgeryToken]
        /// <summary>
        /// Update Golfer Type Detail
        /// </summary>
        /// <param name="golferViewModel"></param>
        /// <returns></returns>
        public JsonResult UpdateGolferType(GolferViewModel golferViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("UpdateGolferProfile", "AdminPanel", golferViewModel);
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
        /// Get All Member Type Detail
        /// </summary>
        /// <returns></returns>
        private List<MemberTypeViewModel> GetAllMemberType()
        {
            MemberTypeViewModel memberTypeViewModel = new MemberTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMemberType", "AdminPanel", memberTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MemberTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<MemberTypeViewModel>();
                }
            }
            else
            {
                return new List<MemberTypeViewModel>();
            }

        }
        /// <summary>
        /// Get All Gender Type Detail
        /// </summary>
        /// <returns></returns>
        private List<GenderTypeViewModel> GetAllGenderType()
        {
            GenderTypeViewModel genderTypeViewModel = new GenderTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllGenderType", "AdminPanel", genderTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GenderTypeViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.GenderTypeViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<GenderTypeViewModel>();
                }
            }
            else
            {
                return new List<GenderTypeViewModel>();
            }

        }
        /// <summary>
        /// Get All Marital Status Detail
        /// </summary>
        /// <returns></returns>
        private List<MaritalStatusViewModel> GetAllMaritalStatusType()
        {
            MaritalStatusViewModel maritalStatusViewModel = new MaritalStatusViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetAllMaritalStatusType", "AdminPanel", maritalStatusViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MaritalStatusViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.MaritalStatusViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<MaritalStatusViewModel>();
                }
            }
            else
            {
                return new List<MaritalStatusViewModel>();
            }

        }



        private List<SalutationViewModel> GetAllSalutationType()
        {
            GenderTypeViewModel genderTypeViewModel = new GenderTypeViewModel()
            {

            };

            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("GetSalutationList", "AdminPanel", genderTypeViewModel);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SalutationViewModel>> dbBookingDetails = JsonConvert.DeserializeObject<ViewModel.ResponseModel.ResponseViewModel<List<ViewModel.SalutationViewModel>>>(data);
                if (dbBookingDetails.IdentityResult.Status == true)
                {
                    return dbBookingDetails.Content;
                }
                else
                {
                    return new List<SalutationViewModel>();
                }
            }
            else
            {
                return new List<SalutationViewModel>();
            }

        }
    }
}