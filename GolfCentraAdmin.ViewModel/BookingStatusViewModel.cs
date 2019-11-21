using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
 public   class BookingStatusViewModel
    {
        public long BookingStatusId { get; set; }
        public string Name { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
    }
}
