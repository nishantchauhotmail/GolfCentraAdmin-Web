using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class CouponViewModel
    {
        public long CouponId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
        public long CouponTypeId { get; set; }
        public decimal Value { get; set; }
        public bool IsFlat { get; set; }
    }
}
