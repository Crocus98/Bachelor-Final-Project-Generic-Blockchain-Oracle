using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using Oracle888730.Utility;
using System;
using System.Threading;

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
                while (true)
                {
                    var changes = await subscribeEvent.GetFilterChanges<SubscribeEventEventDTO>(latestBlock);
                    if (changes.Count > 0)
                    {
                        changes.ForEach(request => {
                            AddSubscriber(request);
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
                StringWriter.Enqueue(message + "[ERROR] Exception stopped the listener thread: " + e.Message);
                Listener();
            }
        }

        //Effettua i controlli e aggiunge il subscriber nel db
        private void AddSubscriber(EventLog<SubscribeEventEventDTO> _request) {
            string address = _request.Event.Sender;
            string serviceType = _request.Event.SubscribeService;
            int serviceTypeValue = (int)_request.Event.SubscribeServiceType;
            try
            {
                ServiceType checkServiceType = OracleContext.GetRequestedType(
                    serviceType,
                    serviceTypeValue
                    );
                if (checkServiceType == null)
                {
                    StringWriter.Enqueue(message + "[ERROR] Failed subscription for non existent service: " + serviceType + " from address: " + address);
                }
                else
                {
                    bool result = OracleContext.AddSubscriber(CreateSubscriber(
                        address,
                        checkServiceType.ServiceTypeId
                        )
                    );
                    if (result)
                    {
                        StringWriter.Enqueue(message + " Successfull subscription for service: " + serviceType + " from address: " + address);
                    }
                    else
                    {
                        StringWriter.Enqueue(message + "[WARNING] User already subscribed for service: " + serviceType + " from address: " + address);
                    }
                }
            }
            catch (Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR]: "+e.Message+". Impossible to add user for service: " + serviceType + " from address: " + address);
            }
        }

        //Crea l'oggetto subscriber per l'inserimento in Db
        private Subscriber CreateSubscriber(string _address, int _serviceTypeId)
        {
            return new Subscriber
            {
                Address = _address,
                ServiceTypeForeignKey = _serviceTypeId
            };
        }
    }
}