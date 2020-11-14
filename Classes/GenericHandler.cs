using Nethereum.Web3;
using System;
using System.Collections;
using System.Threading.Tasks;
using Oracle888730.Utility;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Oracle888730.Contracts.Oracle888730;

namespace Oracle888730.Classes
{
    abstract class GenericHandler<T>
    {
        //protected Web3 web3;
        protected Config config;
        protected Oracle888730Service contractService;
        protected EventLog<T> handledEventLog;
        protected CallAPIHelper callApiHelper;
        protected string message;

        public GenericHandler(Web3 _web3, Config _config, EventLog<T> _eventLog)
        {
            //this.web3 = _web3;
            this.config = _config;
            contractService = new Oracle888730Service(_web3,_config.Oracle.ContractAddress);
            this.handledEventLog = _eventLog;
            callApiHelper = new CallAPIHelper();
        }

        public void Start()
        {
            Task taskHandler = new Task(Handler);
            taskHandler.ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
            taskHandler.Start();
        }

        protected abstract void Handler();

        protected abstract void ExceptionHandler(Task _taskHandler);
    }
}
