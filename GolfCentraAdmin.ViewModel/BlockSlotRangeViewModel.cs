using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class BlockSlotRangeViewModel
    {
        public long BlockSlotRangeId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public long SlotBlockReasonId { get; set; }
        public long CoursePairingId { get; set; }
        public bool IsCustomerAvailable { get; set; }
        public bool IsBookingavailable { get; set; }

        public List<SlotBlockReasonViewModel> SlotBlockReasonViewModels { get; set; }
        public List<CoursePairingViewModel> CoursePairingViewModels { get; set; }
        public List<BlockSlotViewModel> BlockSlotViewModels { get; set; }
        public List<long> DisabledSlot { get; set; }
        public List<long> AlreadyDisabledSlot { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }

        public string SlotBlockReason { get; set; }
        public string CoursePairingName { get; set; }
        public long[] CoursePairingIds { get; set; }


    }
}
