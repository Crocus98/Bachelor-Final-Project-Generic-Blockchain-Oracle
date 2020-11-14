using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Coinbase;
using System.Linq;
using Coinbase.Models;
using System.Threading;
using Oracle888730.Enums;

namespace Oracle888730.Utility
{
    class CallAPIHelper
    {
        CoinbaseClient coinbaseClient;
        public CallAPIHelper()
        {
            //Versione per pagamenti
            //coinbaseClient = new CoinbaseClient(new OAuthConfig { AccessToken = "" });

            //Versione per sole richieste
            coinbaseClient = new CoinbaseClient();
            
        }

        //Ottiene lo spot price per questi cambi
        public async Task<string> GetWantedValue(string _wantedValue)
        {

            //string wantedValue = new CurrencyChangesEnum().EnumStringConversion(_wantedValue);
            try
            {
                if (_wantedValue == "Error")
                {
                    throw new Exception("Wrong conversion value");
                }
                var spot = await coinbaseClient.Data.GetSpotPriceAsync(_wantedValue);
                if(spot.Errors != null)
                {
                    throw new Exception("CoinBase not available");
                }   
                return spot.Data.Amount.ToString();
            }
            catch (Exception e)
            {
                return String.Concat(
                        "Coinbase API call throwing an error: ",
                        e.Message
                    );
            }
        }
    }
}
