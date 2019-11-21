﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class EquipmentViewModel
    {
        public long EquipmentId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
        public List<EquipmentTaxMappingViewModel> EquipmentTaxMappingViewModels { get; set; }
        public List<TaxManagementViewModel> TaxManagementViewModels { get; set; }
        public long[] Taxs { get; set; }
        public bool EquipmentAdded { get; set; }
    }
}
