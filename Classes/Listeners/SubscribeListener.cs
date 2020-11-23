using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.NonceServices;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using Oracle888730.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oracle888730.Classes.Listeners
{
    class SubscribeListener : GenericListener
    {
        public SubscribeListener(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[SubscribeListener]";
        }

        protected override async void Listener()
        {
            Event subscribeEvent = GetEvent("SubscribeEvent");
            HexBigInteger latestBlock = RetrieveLatestBlockToRead(subscribeEvent);
            
            try
            {
                StringWriter.Enqueue(message + " Listener started");
                //INIZIO LOOP 
                while (true)
                {
                    var changes = await subscribeEvent.GetFilterChanges<SubscribeEventEventDTO>(latestBlock);
                    if (changes.Count > 0)
                    {
                        changes.ForEach(request =>
                        {
                            string address = request.Event.Sender;
                            string serviceType = request.Event.SubscribeService;
                            int serviceTypeValue = (int)request.Event.SubscribeServiceType;
                            ServiceType checkServiceType = OracleContext.GetRequestedType(serviceType, serviceTypeValue);
                            if(checkServiceType == null)
                            {
                                StringWriter.Enqueue(message + " Failed subscription for non existent service: " + serviceType + " from address: " + address);
                            }
                            else
                            {
                                OracleContext.AddSubscriber(CreateSubscriber(address, checkServiceType.ServiceTypeId));
                                StringWriter.Enqueue(message + " Successfull subscription for service: " + serviceType + " from address: " + address);
                            }
                        });
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR] Exception stopped the request listener thread " + e.Message);
                Listener();
            }
        }

        private Subscriber CreateSubscriber(string _address, int _serviceTypeId)
        {
            Subscriber addSubscriber = new Subscriber();
            addSubscriber.Address = _address;
            addSubscriber.ServiceTypeForeignKey = _serviceTypeId;
            return addSubscriber;
        }
    }
}