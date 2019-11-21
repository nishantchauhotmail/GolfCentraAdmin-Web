using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using GolfCentraAdmin.ViewModel;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http.Headers;
using GolfCentraAdmin.UI.Common;

namespace GolfCentraAdmin.UI.Controllers
{
    public class AddGolferController : MyBaseController
    {
        // GET: AddGolfer
        public ActionResult Index()
        {
            GolferViewModel golferViewModel = new GolferViewModel()
            {
                MemberTypeViewModels = GetAllMemberType(),
                GenderTypeViewModels = GetAllGenderType(),
                MaritalStatusViewModels = GetAllMaritalStatusType(),
                SalutationViewModels= GetAllSalutationType()
            };

            return View(golferViewModel);
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
        /// Get all Gender Type Detail
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
        /// Get all Marital Status Detail
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
        /// <summary>
        /// Save Golfer Type Detail
        /// </summary>
        /// <param name="golferViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult SaveGolferType(GolferViewModel golferViewModel)
        {
            HttpResponseMessage response = new APIHelper.APICallMethod().GetHttpResponseMessage("SaveGolferDetails", "AdminPanel", golferViewModel);
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


        public ActionResult ImportGolferExcel(GolferViewModel model)
        {
            try
            {
                 string str1 = Guid.NewGuid().ToString();

                HttpPostedFileBase file2 = model.Attachment;
                if (file2.ContentLength != 0)
                {
                    string path = "~/Uploads/Images";
                    if (!Directory.Exists(this.Server.MapPath(path)))
                        Directory.CreateDirectory(this.Server.MapPath(path));
                    string extension = Path.GetExtension(file2.FileName);
                    string str2 = Path.Combine(this.Server.MapPath(path), Path.GetFileName(str1 + extension));
                    if (!str2.EndsWith(".xls") && !str2.EndsWith(".xlsx"))
                        return this.Content("{\"name\":\"Please Choose A File Of Type .xls Or .xlsx\",\"code\":\"-1\" }", "application/json");
                    file2.SaveAs(str2);

                    HttpClient httpClient = new HttpClient();
                    int int32 = Convert.ToInt32(Constants.ApiAccess.HttpTime);
                    httpClient.Timeout = TimeSpan.FromMinutes((double)int32);
                    httpClient.BaseAddress = new Uri(Constants.Url.WebApiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                    KeyValuePair<string, string>[] keyValuePairArray = new KeyValuePair<string, string>[2]
                    {
              new KeyValuePair<string, string>("Foo", "Bar"),
              new KeyValuePair<string, string>("More", "Less")
                    };
                    foreach (KeyValuePair<string, string> keyValuePair in keyValuePairArray)
                        multipartFormDataContent.Add((HttpContent)new StringContent(keyValuePair.Value), keyValuePair.Key);
                    ByteArrayContent byteArrayContent = new ByteArrayContent(System.IO.File.ReadAllBytes(str2));
                    byteArrayContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "attachment",
                        FileName = "Foo" + extension
                    };
                    multipartFormDataContent.Add((HttpContent)byteArrayContent);
                    HttpResponseMessage result = httpClient.PostAsync("api/ExeclUpload/UploadExcel", (HttpContent)multipartFormDataContent).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<List<GolferViewModel>>(data);
                        string convertedData = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/AddGolfer/_FailedRecord.cshtml", Response);
                        return Content(convertedData);
                    }
                    List<GolferViewModel> golfer = new List<GolferViewModel>() { new GolferViewModel() { Message = "Failed" } };
                    string convertedData1 = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/AddGolfer/_FailedRecord.cshtml", Response);
                    return Content(convertedData1);
                }

                List<GolferViewModel> golfer2 = new List<GolferViewModel>() { new GolferViewModel() { Message = "Failed" } };
                string convertedData2 = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/AddGolfer/_FailedRecord.cshtml", Response);
                return Content(convertedData2);
            }
            catch (Exception ex)
            {
                List<GolferViewModel> golfer = new List<GolferViewModel>() { new GolferViewModel() { Message = "Failed" } };
                string convertedData1 = Common.HtmlHelper.RenderViewToString(this.ControllerContext, "~/Views/AddGolfer/_FailedRecord.cshtml", Response);
                return Content(convertedData1);
            }
        }

        public ActionResult ImportExecl()
        {
            return View();
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