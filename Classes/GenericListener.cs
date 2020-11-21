using Nethereum.Contracts;
using Nethereum.Web3;
using System.Collections;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Utility;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using Nethereum.RPC.NonceServices;

namespace Oracle888730.Classes
{
    abstract class GenericListener : IGenericListener
    {
        protected Web3 web3;
        protected Config config;
        protected Oracle888730Service contractService;
        protected string message;
        protected string handlersNameSpace;
        protected Dictionary<string, Type> handlers;
        protected Account account;
        protected InMemoryNonceService inMemoryNonceService;

        public GenericListener(Web3 _web3, Config _config, Account _account, InMemoryNonceService _inMemoryNonceService)
        {
            this.web3 = _web3;
            this.config = _config;
            contractService = new Oracle888730Service(web3, config.Oracle.ContractAddress);
            handlersNameSpace = "Classes.Handlers";
            handlers = new Dictionary<string, Type>();
            account = _account;
            inMemoryNonceService = _inMemoryNonceService;
        }

        public Thread Start()
        {
            Thread threadListener = new Thread(Listener);
            threadListener.Start();
            return threadListener;  
        }

        protected Event GetEvent(string _eventName)
        {
            StringWriter.Enqueue(message + " Listener setup started...");
            var contract = web3.Eth.GetContract(
                config.Oracle.Abi,
                config.Oracle.ContractAddress
            );
            Event genericEvent = contract.GetEvent(_eventName);
            return genericEvent;
        }

        protected HexBigInteger RetrieveLatestBlockToRead(Event _event)
        {
            HexBigInteger latestBlock;
            var filter = _event.CreateFilterAsync();
            filter.Wait();
            latestBlock = filter.Result;
            return latestBlock;
            /*if (config.Oracle.LatestBlock == null)
            {
                var filter = _subscribeEvent.CreateFilterAsync();
                filter.Wait();
                latestBlock = filter.Result;
                config.Oracle.LatestBlock = latestBlock.HexValue;
                config.Save();
            }
            else
            {
                latestBlock = new HexBigInteger(config.Oracle.LatestBlock);
            }*/
        }

        protected abstract void Listener();
    }
}
