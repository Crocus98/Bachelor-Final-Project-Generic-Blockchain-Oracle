using Nethereum.Contracts;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle888730.Classes
{
    abstract class GenericRequestHandler : GenericHandler<RequestEventEventDTO>
    {
        public GenericRequestHandler(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[RequestHandler]";
        }

        protected abstract void HandleSingleRequest(EventLog<RequestEventEventDTO> _eventLog);
    }
}
