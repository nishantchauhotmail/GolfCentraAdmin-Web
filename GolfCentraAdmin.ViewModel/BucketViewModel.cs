using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class BucketViewModel
    {
        public long BucketDetailId { get; set; }
        public int Balls { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
        public decimal Price { get; set; }

        public Nullable<long> DayTypeId { get; set; }
        public Nullable<long> MemberTypeId { get; set; }
        public List<BucketTaxMappingViewModel> BucketTaxMappingViewModels { get; set; }
        public List<TaxManagementViewModel> TaxManagementViewModels { get; set; }
        public long[] Taxs { get; set; }
        public List<DayTypeViewModel> DayTypeViewModels { get; set; }
        public List<MemberTypeViewModel> MemberTypeViewModels { get; set; }

        public string DayTypeName { get; set; }
        public string MemberTypeName { get; set; }
        public DateTime Date { get; set; }
    }
}
