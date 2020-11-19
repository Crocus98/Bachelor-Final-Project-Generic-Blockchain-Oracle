using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using System;
using System.Threading;
using System.Threading.Tasks;
using Oracle888730.Utility;

using Nethereum.ABI.FunctionEncoding.Attributes;
using Oracle888730.Classes.Handlers;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Reflection;

namespace Oracle888730.Classes.Listeners
{
    class RequestListener : GenericListener
    {
        public RequestListener(Web3 _web3, Config _config) : base(_web3, _config)
        {
            message = "[RequestListener]";
        }

        protected override async void Listener()
        {
            try
            {
                Event requestEvent = GetEvent("RequestEvent");
                HexBigInteger latestBlock = RetrieveLatestBlockToRead(requestEvent);
                StringWriter.Enqueue(message + " Listener started");
                while (true)
                {
                    var changes = await requestEvent.GetFilterChanges<RequestEventEventDTO>(latestBlock);
                    if (changes.Count > 0)
                    {
                        changes.ForEach(x => {
                            string service = x.Event.RequestService.ToUpper();
                            Type type;
                            if (handlers.ContainsKey(service))
                            {
                                type = handlers.GetValueOrDefault(service);
                            }
                            else
                            {
                                type = ModulesHelper.GetType(service, handlersNameSpace);
                                handlers.Add(service,type);
                            }
                            if (type != null)
                            {
                                IGenericHandler currentType = ModulesHelper.GetInstance<IGenericHandler>(type, new object[] { web3, config, message });
                                currentType.Start(x.Event);
                            }
                        });
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR] Exception stopped the request listener thread " + e.Message );
                Listener();
            }
            
        }

        private HexBigInteger RetrieveLatestBlockToRead(Event _requestEvent)
        {
            // TODO
            HexBigInteger latestBlock;
            var filter = _requestEvent.CreateFilterAsync();
            filter.Wait();
            latestBlock = filter.Result;
            return latestBlock;
            /*if (config.Oracle.LatestBlock == null)
            {
                var filter = _requestEvent.CreateFilterAsync();
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

    }

}