using Nethereum.Web3;
using Oracle888730.Utility;
using System;
using System.Threading.Tasks;

namespace Oracle888730.Classes
{
    class SubscribeListener : GenericListener 
    {
        public SubscribeListener(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[SubscribeListener]";
        }

        protected override void ExceptionHandler(Task _taskRequestListener)
        {
            var exception = _taskRequestListener.Exception;
            Console.WriteLine(message+"[ERROR] " + exception.Message);
        }

        private void TasksExceptionHandler(Task _taskHandler)
        {
            var exception = _taskHandler.Exception;
            Console.WriteLine(message+"[ERROR] " + exception.Message);
        }
        protected override void Listener()
        {
            /*
            using (var db = new OracleContext())
            {
                db.AddSubscriber(new Subscriber
                {
                    Address = address,
                    RequestType = 99
                });
            }*/
        }
    }
}
