﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
  public  class CourseTaxMappingViewModel
    {
        public long TaxId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
    }
}