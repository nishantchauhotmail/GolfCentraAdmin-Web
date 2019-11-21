using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
   public  class EquipmentTaxMappingViewModel
    {
        public long EquipmentTaxMappingId { get; set; }
        public long TaxId { get; set; }
        public long EquipmentId { get; set; }
        public string TaxName { get; set; }
        public decimal TaxPercentage { get; set; }
    }
}
