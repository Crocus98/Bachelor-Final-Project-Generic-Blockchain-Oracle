using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Oracle888730.Utility.ApiHelpers
{

    //Classe Stub per testare la molteplicità di servizi
    public class COINMARKETCAPApiHelper : GenericAPIHelper
    {
        public COINMARKETCAPApiHelper() : base()
        {
            message = "[CoinmarketcapApiHelper]";
        }

        public override string GetWantedValue(string _wantedChange)
        {
            Thread.Sleep(50);
            return "100.00 " + _wantedChange;
        }
    }
}
