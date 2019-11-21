using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
  public  class PricingTaxMappingViewModel
    {
        public long PriceTaxMappingId { get; set; }
        public long TaxId { get; set; }
        public long PricingId { get; set; }
        public long FeeCategoryId { get; set; }
        public string TaxName { get; set; }
        public string Percentage { get; set; }
        public string FeeCategoryName { get; set; }
    }
}
