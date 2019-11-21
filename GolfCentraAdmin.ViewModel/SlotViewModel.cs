using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class SlotViewModel
    {
        public long SlotId { get; set; }
        public System.TimeSpan Time { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
        public long SessionId { get; set; }
        public long BookingTypeId { get; set; }
        public DateTime Date { get; set; }
        public long? CoursePairingId { get; set; }
        public int PlayerLeftCount { get; set; }
    }
}
