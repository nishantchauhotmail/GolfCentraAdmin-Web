using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCentraAdmin.ViewModel.ResponseModel
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel()
        {

        }
        public IdentityResult IdentityResult { get; set; }

        public T Content { get; set; }
    }
}