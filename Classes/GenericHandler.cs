using Nethereum.Web3;
using System;
using System.Collections;
using System.Threading.Tasks;
using Oracle888730.Utility;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Oracle888730.Contracts.Oracle888730;
using System.Collections.Generic;
using System.Threading;
using Oracle888730.OracleEF.Models;

namespace Oracle888730.Classes
{
    abstract class GenericHandler<T>
    {
        //protected Web3 web3;
        protected Config config;
        protected Oracle888730Service contractService;
        protected CallAPIHelper callApiHelper;
        protected string message;

        public GenericHandler(Web3 _web3, Config _config)
        {
            this.config = _config;
            this.contractService = new Oracle888730Service(_web3,_config.Oracle.ContractAddress);
            callApiHelper = new CallAPIHelper();
        }

        public Thread Start()
        {
            Thread threadHandler = new Thread(Handler);
            threadHandler.Start();
            return threadHandler;
        }

        protected abstract void Handler();

        protected virtual void HandleSingleRequest(EventLog<T> _eventLog)
        {

        }

    }
}
