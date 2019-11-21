using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel.ResponseModel
{
    public class IdentityError
    { /// <summary>
      /// Gets or sets the code for this error.
      /// </summary>
      /// <value>
      /// The code for this error.
      /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        /// <value>
        /// The description for this error.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; set; }
    }

  
}
