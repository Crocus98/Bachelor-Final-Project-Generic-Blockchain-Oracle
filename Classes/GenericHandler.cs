using Nethereum.Web3;
using System;
using Oracle888730.Utility;
using Oracle888730.Contracts.Oracle888730;
using System.Threading;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Nethereum.RPC.NonceServices;
using Nethereum.Web3.Accounts;
using System.Collections;
using System.Collections.Generic;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Oracle888730.Classes
{
    abstract class GenericHandler
    {
        protected string message;
        protected Oracle888730Service contractService;
        protected GenericAPIHelper apiHelper;
        protected Web3 web3;
        protected Config config;

        public GenericHandler(Web3 _web3, Config _config)
        {
            contractService = new Oracle888730Service(_web3, _config.Oracle.ContractAddress);
            web3 = _web3;
            config = _config;
        }

        public void Start(RequestEventEventDTO _event)
        {
            Thread requestHandlerThread = new Thread(Handler);
            requestHandlerThread.Start();
        }

        protected abstract void Handler();
    }
}
