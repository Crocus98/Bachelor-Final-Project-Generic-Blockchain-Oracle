using Coinbase;
using System;
using System.Threading.Tasks;

namespace Oracle888730.Utility.ApiHelpers
{
    public class COINBASEAPIHelper : GenericAPIHelper
    {
        private readonly CoinbaseClient coinbaseClient;

        public COINBASEAPIHelper() : base()
        {
            message = "[CoinbaseApiHelper]";
            //Instanzio l'oggetto CoinBaseClient
            //Versione semplice per sole richieste
            coinbaseClient = new CoinbaseClient();
            //Versione con token e oAuth per poter effettuare anche pagamenti
            //coinbaseClient = new CoinbaseClient(new OAuthConfig { AccessToken = "" });
        }

        //Questo metodo ottiene lo spot price per i cambi supportati
        private async Task<string> GetCoinbaseValue(string _wantedChange)
        {
            var spot = await coinbaseClient.Data.GetSpotPriceAsync(_wantedChange);
            return spot.Data.Amount.ToString() + " " + spot.Data.Currency;
        }

        public override string GetWantedValue(string _wantedChange)
        {
            try
            {
                var result = GetCoinbaseValue(_wantedChange);
                result.Wait();
                return result.Result;
            }
            catch
            {
                throw new Exception("Coinbase not available (or invalid currency pair string, check that database datas are correct)");
            }
        }
    }
}
