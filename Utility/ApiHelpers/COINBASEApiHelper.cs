using Coinbase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oracle888730.Utility.ApiHelpers
{
    class COINBASEAPIHelper : GenericAPIHelper
    {
        CoinbaseClient coinbaseClient;

        public COINBASEAPIHelper() : base()
        {
            //Instanzio l'oggetto CoinBaseClient
            //Versione semplice per sole richieste
            coinbaseClient = new CoinbaseClient();
            //Versione con token e oAuth per poter effettuare anche pagamenti
            //coinbaseClient = new CoinbaseClient(new OAuthConfig { AccessToken = "" });
        }

        //Questo metodo ottiene lo spot price per i cambi supportati
        public async Task<string> GetWantedValue(string _wantedChange)
        {
            var spot = await coinbaseClient.Data.GetSpotPriceAsync(_wantedChange);
            if (spot.Errors != null)
            {
                throw new Exception("CoinBase not available");
            }
            else if (spot.Data == null)
            {
                throw new Exception("Invalid currency pair string");
            }
            return spot.Data.Amount.ToString() + " " + spot.Data.Currency;
        }
    }
}
