using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class BlockSlotViewModel
    {
        public long BlockSlotId { get; set; }
        public long BlockSlotRangeId { get; set; }
        public long SlotSessionWiseId { get; set; }
        public string SlotTime { get; set; }
        public bool IsAlreadyDisabled { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }

    }
}
