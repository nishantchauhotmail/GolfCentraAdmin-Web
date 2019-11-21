using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel.ResponseModel
{
    public class IdentityResult
    {

        /// <summary>
        /// Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Status { get;  set; }


        /// <summary>
        /// 
        /// </summary>
        /// <value> .</value>
        public string Message { get; set; }

        /// <summary>
        /// An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s containing an errors
        /// that occurred during the identity operation.
        /// </summary>
        /// <value>An <see cref="IEnumerable{T}"/> of <see cref="IdentityError"/>s.</value>
        public IEnumerable<IdentityError> Errors
        {
            get; set;

        }


    }
}
