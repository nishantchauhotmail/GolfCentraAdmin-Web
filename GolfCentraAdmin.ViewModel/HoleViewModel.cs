using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
  public  class HoleViewModel
    {
        public long HoleInformationId { get; set; }
        public long HoleNumberId { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Storke { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ImgURL { get; set; }
        public List<TeeViewModel> TeeViewList { get; set; }
        public ApiClientViewModel ApiClientViewModel { get; set; }
    }
}
