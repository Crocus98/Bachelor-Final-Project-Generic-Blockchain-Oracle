using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.Utility;
using Oracle888730.Utility.ApiHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle888730.Classes.Handlers
{
    class COINMARKETCAPHandler : GenericHandler
    {
        public COINMARKETCAPHandler(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[CoinbaseHandler]";
            apiHelper = new COINMARKETCAPApiHelper();
        }

        protected override void Handler(RequestEventEventDTO _event)
        {
            throw new NotImplementedException();
        }
    }
}
