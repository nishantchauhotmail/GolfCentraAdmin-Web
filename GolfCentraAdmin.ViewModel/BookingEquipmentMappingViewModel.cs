using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
  public  class BookingEquipmentMappingViewModel
    {
        public long BookingEquipmentMappingId { get; set; }
        public long BookingId { get; set; }
        public long EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public Nullable<decimal> EquipmentPrice { get; set; }
        public int EquipmentCount { get; set; }
        public int EquipmentLeft { get; set; }
    }
}
