using Nethereum.Contracts;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Utility;
using System.Threading.Tasks;
using System.Threading;

namespace Oracle888730.Classes
{
    abstract class GenericListener
    {
        protected Web3 web3;
        protected Config config;
        protected Oracle888730Service contractService;
        protected string message;

        public GenericListener(Web3 _web3, Config _config)
        {
            this.web3 = _web3;
            this.config = _config;
            contractService = new Oracle888730Service(web3, config.Oracle.ContractAddress);
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

        protected abstract void Listener();
    }
}
