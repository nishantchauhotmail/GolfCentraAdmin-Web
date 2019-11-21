using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GolfCentraAdmin.ViewModel
{
  public  class GolferViewModel
    {
        public long GolferId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string ClubMemberId { get; set; }
        public string MemberShipId { get; set; }
        public string Mobile { get; set; }
         public string PhoneCode { get; set; }
        public Nullable<System.DateTime> MemberSince { get; set; }
        public string Occpoution { get; set; }
        public Nullable<long> MaritalStatusId { get; set; }
        public string SpouseName { get; set; }
        public string EmailAddress { get; set; }
        public string PlatformName { get; set; }
        public string SpecialComments { get; set; }
        public Nullable<long> MemberTypeId { get; set; }
        public Nullable<long> GenderTypeId { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
        public List<GenderTypeViewModel> GenderTypeViewModels { get; set; }
        public List<MemberTypeViewModel> MemberTypeViewModels { get; set; }
        public List<MaritalStatusViewModel> MaritalStatusViewModels{ get; set; }


        public bool IsBlocked { get; set; }
      //  public string Attachment { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
        public long UpdateId { get; set; }
        public string SelectBoxDisplay { get; set; }
        public ImportGolferExcelReportModel ImportGolferExcelReportModel { get; set; }
        public string Message { get; set; }
        public List<SalutationViewModel> SalutationViewModels { get; set; }
        public DateTime DOB { get; set; }
        public long SalutationId { get; set; }
    }
}
