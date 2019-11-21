using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
  public  class CoursePairingViewModel
    {
        public long CoursePairingId { get; set; }
        public long StartCourseNameId { get; set; }
        public Nullable<long> EndCourseNameId { get; set; }
        public long HoleTypeId { get; set; }
        public string Value { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
        public string CoursePairingName { get; set; }

    }
}
