using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Oracle888730.Utility;
using System;
using System.Threading.Tasks;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using System.Numerics;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Oracle888730.Classes.Handlers
{
    class RequestHandler : GenericRequestHandler
    {
        private static List<EventLog<RequestEventEventDTO>> handledEventLogList;

        public RequestHandler(Web3 _web3, Config _config) : base (_web3, _config)
        {
            handledEventLogList = new List<EventLog<RequestEventEventDTO>>();
        }

        public static void Enqueue(List<EventLog<RequestEventEventDTO>> _requests)
        {
            //Monitor.TryEnter(handledEventLogList);
            lock (handledEventLogList) {
                handledEventLogList.AddRange(_requests);
            }
            //Monitor.Exit(handledEventLogList);
        }

        protected override void Handler()
        {
            while (true)
            {
                List<EventLog<RequestEventEventDTO>> temporaryList;
                //Monitor.TryEnter(handledEventLogList);
                lock (handledEventLogList) { 
                    temporaryList = new List<EventLog<RequestEventEventDTO>>(handledEventLogList);
                    handledEventLogList.Clear();
                }
                //Monitor.Exit(handledEventLogList);
                if(temporaryList.Count > 0)
                {
                    temporaryList.ForEach(changes => {
                        HandleSingleRequest(changes);
                    });
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        protected override void HandleSingleRequest(EventLog<RequestEventEventDTO> _eventLog)
        {
            string requestType = _eventLog.Event.RequestService;
            int requestTypeValue = (int)_eventLog.Event.RequestServiceType;
            string address = _eventLog.Event.Sender;

            var service = ModulesHelper.GetInstance(requestType);
            if(service == null)
            {
                StringWriter.Enqueue(message + " Failed request for inexistent service: " + requestType + " From: " + address);
            } else
            {
                // TODO Restituisci risultato
                StringWriter.Enqueue(message + " Successfull request for service: " + requestType + " From: " + address);
            }

            //string stringForApi = new CurrencyChangesEnum().EnumStringConversion(requestType);
            //if (stringForApi == "Error")
            //{
            //    StringWriter.Enqueue(message + " Failed request for inexistent service: " + requestType + " From: " + address);
            //}
            //else
            //{
            //    var value = callApiHelper.GetWantedValue(stringForApi);
            //    value.Wait();
            //    //Using the same instance of Web3 across services, there is a nonce manager (in memory) that will ensure that your transactions are in the right order (safe thread too)
            //    var res = contractService.SendResponseRequestAndWaitForReceiptAsync(
            //            clientAddress: address,
            //            value: value.Result,
            //            service: ,
            //            serviceType: 
            //        );
            //    StringWriter.Enqueue(message + " Successfull request for service: " + requestType + " From: " + address);
            //}
        }
    }
}
