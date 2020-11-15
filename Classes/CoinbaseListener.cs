using Microsoft.EntityFrameworkCore;
using Nethereum.Contracts;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.Enums;
using Oracle888730.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oracle888730.Classes
{
    class CoinbaseHandler : GenericHandler<SubscribeEventEventDTO>
    {
        private CallAPIHelper callApiHelper;

        public CoinbaseHandler(Web3 _web3, Config _config) : base (_web3, _config)
        {
            callApiHelper = new CallAPIHelper();
        }

        protected override void Handler()
        {
            while (true)
            {
                object waitTimeOut = new object();
                lock (waitTimeOut)
                {
                    Monitor.Wait(waitTimeOut, TimeSpan.FromMilliseconds(3600 * 1000));
                }
                
            }
        }

        protected override void HandleSingleRequest(EventLog<SubscribeEventEventDTO> _eventLog)
        {
            //Da fare
            int requestType = (int)_eventLog.Event.RequestType;
            string address = _eventLog.Event.Sender;
            string stringForApi = new CurrencyChangesEnum().EnumStringConversion(requestType);
            if (stringForApi == "Error")
            {
                StringWriter.Enqueue(message + " Failed request for inexistent service: " + requestType + " From: " + address);
            }
            else
            {
                var value = callApiHelper.GetWantedValue(stringForApi);
                value.Wait();
                //Using the same instance of Web3 across services, there is a nonce manager (in memory) that will ensure that your transactions are in the right order (safe thread too)
                var res = contractService.SendResponseRequestAndWaitForReceiptAsync(
                        clientAddress: address,
                        value: value.Result,
                        requestType: requestType
                    );
                StringWriter.Enqueue(message + " Successfull request for service: " + requestType + " From: " + address);
            }
        }
    }
    
}
