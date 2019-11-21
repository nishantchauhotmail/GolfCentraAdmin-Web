using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel
{
   public class DashBoardTopBarViewModel
    {
        public int TotalBooking { get; set; }
        public int Hole9Booking { get; set; }
        public int Hole18Booking { get; set; }
        public int Hole27Booking { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
