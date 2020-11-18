using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle888730.Classes.Handlers
{
    class NewAllHandler : GenericHandler<RequestEventEventDTO>
    {
        public NewAllHandler(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[NewAllHandler]";
            //handledEventLogList = new List<EventLog<RequestEventEventDTO>>();
        }
        protected override void Handler()
        {
            throw new NotImplementedException();
        }
    }
}
