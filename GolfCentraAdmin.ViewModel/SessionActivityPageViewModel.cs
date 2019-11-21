﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
  public  class SessionActivityPageViewModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public long LoginHistoryId { get; set; }
        public string PerformOn { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
    }
}
