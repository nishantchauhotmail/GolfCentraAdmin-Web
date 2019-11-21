using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class PageViewModel
    {
        public Int64 PageId { get; set; }
        public string PageName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public Int64 ParentId { get; set; }
        public int Ordering { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
    }
}
