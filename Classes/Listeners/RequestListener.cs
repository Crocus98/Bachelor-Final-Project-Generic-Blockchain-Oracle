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
                        RequestHandler.Enqueue(changes);
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

        private HexBigInteger RetrieveLatestBlockToRead(Event _requestEvent)
        {
            HexBigInteger latestBlock;
            var filter = _requestEvent.CreateFilterAsync();
            filter.Wait();
            latestBlock = filter.Result;
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
            return latestBlock;
        }

    }

}