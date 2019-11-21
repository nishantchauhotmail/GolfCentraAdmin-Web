using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
    public class ScoreSearchViewModel
    {
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long SearchTypeId { get; set; }
        public string[] EmailList { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }


        public long CoursePairingId { get; set; }
        public string GolferName { get; set; }
        public string MemberShipId { get; set; }
        public List<CoursePairingViewModel> CoursePairingViewModels { get; set; }
      
    }
}
