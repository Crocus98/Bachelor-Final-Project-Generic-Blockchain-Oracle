using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Utility;
using System.Threading.Tasks;

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

        public void Start()
        {
            Task taskListener = new Task(Listener);
            taskListener.ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
            taskListener.Start();
            taskListener.Wait();
        }

        protected abstract void Listener();

        protected abstract void ExceptionHandler(Task _taskListener);
    }
}
