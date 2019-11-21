using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class TeeViewModel
    {
        public long TeeId { get; set; }
        public long HoleTeeYardageId { get; set; }
        public string TeeName { get; set; }
        public decimal Yardage { get; set; }
    }
}
