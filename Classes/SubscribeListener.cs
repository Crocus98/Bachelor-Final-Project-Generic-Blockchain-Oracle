using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.Enums;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using Oracle888730.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Oracle888730.Enums.CurrencyChangesEnum;

namespace Oracle888730.Classes
{
    class SubscribeListener : GenericListener
    {
        public SubscribeListener(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[SubscribeListener]";
        }

        protected override async void Listener()
        {
            try
            {
                Event subscribeEvent = GetEvent("SubscribeEvent");
                HexBigInteger latestBlock = RetrieveLatestBlockToRead(subscribeEvent);
                StringWriter.Enqueue(message + " Listener started");
                CurrencyChangesEnum currencyEnum = new CurrencyChangesEnum();
                //INIZIO LOOP 
                while (true)
                {
                    var changes = await subscribeEvent.GetFilterChanges<SubscribeEventEventDTO>(latestBlock);
                    if (changes.Count > 0)
                    {
                        changes.ForEach(request =>
                        {
                            string address = request.Event.Sender;
                            int requestType = (int)request.Event.RequestType;
                            if(Enum.IsDefined(typeof(CurrencyChanges), requestType))
                            {
                                using (var db = new OracleContext())
                                {
                                    db.AddSubscriber(new Subscriber
                                    {
                                        Address = address,
                                        RequestType = requestType
                                    });
                                }
                                StringWriter.Enqueue(message + " Successfull subscription for service: " + requestType + " from address: " + address);
                            }
                            else
                            {
                                StringWriter.Enqueue(message + " Failed subscription for non existent service: " + requestType + " from address: " + address);
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
                throw e;
            }
        }

        private HexBigInteger RetrieveLatestBlockToRead(Event _subscribeEvent)
        {
            HexBigInteger latestBlock;
            var filter = _subscribeEvent.CreateFilterAsync();
            filter.Wait();
            latestBlock = filter.Result;
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
            return latestBlock;
        }
    }
}