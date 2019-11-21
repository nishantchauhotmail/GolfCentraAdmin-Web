using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
   public class WDDRUpdatePriceViewModel
    {
        public decimal TaxAndFee { get; set; }
        public decimal BucketFeePerPlayer { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal RangeFee { get; set; }
    }
}
