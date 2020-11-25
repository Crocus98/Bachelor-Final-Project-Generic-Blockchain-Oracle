using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Coinbase;
using System.Linq;
using Coinbase.Models;
using System.Threading;

namespace Oracle888730.Utility
{
    public abstract class GenericAPIHelper
    {
        protected string nameSpace;
        protected string message;
        public GenericAPIHelper()
        {
            nameSpace = "Utility.ApiHelpers";
        }

        //Ritorna il valore voluto dall'API chiamata. La stringa potrebbe anche essere un JSON
        public abstract string GetWantedValue(string _wantedChange);
    }
}
