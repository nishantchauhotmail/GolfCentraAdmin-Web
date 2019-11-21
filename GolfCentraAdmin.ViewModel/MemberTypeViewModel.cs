using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
   public class MemberTypeViewModel
    {
        public long MemberTypeId { get; set; }
        public string Name { get; set; }
        public string ValueToShow { get; set; }
        public int PlayerCount { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
    }
}
