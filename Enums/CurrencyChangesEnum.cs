using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle888730.Enums
{
    class CurrencyChangesEnum
    {
        public enum CurrencyChanges
        {
            ETHUSD = 1,
            BTCUSD = 2,
            XRPUSD = 3
        }

        public string EnumStringConversion(int _switch)
        {
            string wantedValue;
            switch (_switch)
            {
                case 1:
                    wantedValue = "ETH-USD";
                    break;
                case 2:
                    wantedValue = "BTC-USD";
                    break;
                case 3:
                    wantedValue = "XRP-USD";
                    break;
                default:
                    wantedValue = "Error";
                    break;
            }
            return wantedValue;
        } 
    }
}
