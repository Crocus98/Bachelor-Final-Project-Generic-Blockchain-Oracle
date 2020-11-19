using Nethereum.Web3;
using System;
using Oracle888730.Utility;
using Oracle888730.Contracts.Oracle888730;
using System.Threading;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;

namespace Oracle888730.Classes
{
    abstract class GenericHandler : IGenericHandler
    {
        protected string message;
        protected Oracle888730Service contractService;
        protected GenericAPIHelper apiHelper;

        public GenericHandler(Web3 _web3, Config _config)
        {
            contractService = new Oracle888730Service(_web3, _config.Oracle.ContractAddress);
        }

        public void Start(RequestEventEventDTO _event)
        {

            Thread requestHandlerThread = new Thread(() => Handler(_event));
            requestHandlerThread.Start();
        }

        protected abstract void Handler(RequestEventEventDTO _event);
    }
}
