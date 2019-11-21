using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class PromotionViewModel
    {
        public long PromotionsId { get; set; }
        public string Name { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public long HoleTypeId { get; set; }
        public bool GreenFee { get; set; }
        public bool CaddieFee { get; set; }
        public bool CartFee { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Extra { get; set; }
        public List<EquipmentViewModel> EquipmentViewModel { get; set; }
        public long[] Taxs { get; set; }
        public bool Hole9 { get; set; }
        public bool Hole18 { get; set; }
        public bool Hole27 { get; set; }
        public long[] EquipmentIds { get; set; }
        public List<TaxManagementViewModel> TaxManagementViewModels { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }

        public string EquipmentName { get; set; }
        public string HoleTypeName { get; set; }
    }
}
