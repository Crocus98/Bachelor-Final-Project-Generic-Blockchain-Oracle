using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using System;
using System.Threading;
using Oracle888730.Utility;
using System.Collections.Generic;
using Nethereum.RPC.NonceServices;
using Nethereum.Web3.Accounts;
using System.Reflection;
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
                        changes.ForEach(x => {
                            MainHandler.EnqueueEvent(x.Event);
                        });
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR] Exception stopped the listener thread " + e.Message );
                Listener();
            }
            
        }
    }

}