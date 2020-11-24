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
    abstract class GenericListener : IGeneric
    {
        protected Web3 web3;
        protected Config config;
        protected Oracle888730Service contractService;
        protected string message;

        public GenericListener(Web3 _web3, Config _config)
        {
            web3 = _web3;
            config = _config;
            contractService = new Oracle888730Service(web3, config.Oracle.ContractAddress);
        }

        public Thread Start()
        {
            Thread threadListener = new Thread(Listener);
            threadListener.Start();
            return threadListener;
        }

        //Metodo che ottiene il tipo di evento che viene rilevato dalla classe listener figlia
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

        //Metodo che ottiene il blocco da cui iniziare a rilevare gli eventi scritti su blockchain
        protected HexBigInteger RetrieveLatestBlockToRead(Event _event)
        {
            HexBigInteger latestBlock;
            var filter = _event.CreateFilterAsync();
            filter.Wait();
            latestBlock = filter.Result;
            return latestBlock;
        }

        protected abstract void Listener();
    }
}
