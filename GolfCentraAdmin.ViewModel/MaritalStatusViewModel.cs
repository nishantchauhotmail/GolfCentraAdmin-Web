using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class MaritalStatusViewModel
    {
        public long MaritalStatusId { get; set; }
        public string Value { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
    }
}
