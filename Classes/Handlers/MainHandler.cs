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
using Org.BouncyCastle.Ocsp;
using Oracle888730.Utility.ApiHelpers;
using Flurl.Util;
using Nethereum.RPC.Eth.DTOs;
using Oracle888730.Contracts.Oracle888730;
using Nethereum.RPC.NonceServices;
using Nethereum.Web3.Accounts;
using Nethereum.RPC.Web3;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;

namespace Oracle888730.Classes.Handlers
{
    class MainHandler : GenericHandler
    {
        protected static Queue<RequestEventEventDTO> queueEventsToHandle;
        protected Dictionary<string, Type> apiHelpersTypes;
        protected string apiHelpersNamespace = "Utility.ApiHelpers";

        public MainHandler(Web3 _web3, Config _config ) : base (_web3, _config)
        {
            message = "[MainHandler]";
            queueEventsToHandle = new Queue<RequestEventEventDTO>();
            apiHelpersTypes = new Dictionary<string, Type>();
        }

        protected override void Handler()
        {
            List<RequestEventEventDTO> tempEventList = null;
            try
            {
                StringWriter.Enqueue(message + " Handler setup started...");
                Queue<Oracle888730Service> services = new Queue<Oracle888730Service>();
                services.Enqueue(contractService);
                config.RpcServer.SecondaryAddresses.ToList().ForEach(x =>
                {
                    Account secondaryAccount = new Account(x[1]);
                    Web3 secondaryAccountWeb3 = new Web3(secondaryAccount, config.RpcServer.Url);
                    services.Enqueue(new Oracle888730Service(secondaryAccountWeb3, config.Oracle.ContractAddress));
                });
                ThreadPool.SetMinThreads(0, 0);
                ThreadPool.SetMaxThreads(services.Count(), 0);
                StringWriter.Enqueue(message + " Handler started with "+ services.Count() + " services (Threads)");
                while (true)
                {
                    tempEventList = GetList();
                    if(tempEventList == null)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    tempEventList.ForEach(x => {
                        Oracle888730Service freeService = DequeueService(services);
                        ThreadPool.QueueUserWorkItem(threadFunction => HandleMethod(x, freeService, services));
                    });
                }
            }
            catch (Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR] " + e.Message);
                if (tempEventList != null)
                {
                    EnqueueEvent(tempEventList);
                }
                Handler();
            }
        }

        private void HandleMethod(RequestEventEventDTO _eventToHandle, Oracle888730Service _service, Queue<Oracle888730Service> _services)
        {
            try
            {
                bool checkContainsKey;
                lock (apiHelpersTypes)
                {
                    checkContainsKey = apiHelpersTypes.ContainsKey(_eventToHandle.RequestService);
                }
                // TODO Separe void CheckConstainsKey
                if (!checkContainsKey)
                {
                    Type apiHelperTypeForDictionary = ModulesHelper.GetType(_eventToHandle.RequestService, apiHelpersNamespace);
                    if(apiHelperTypeForDictionary == null)
                    {
                        throw new Exception("There is no service like: "+_eventToHandle.RequestService + " Asked from: " + _eventToHandle.Sender);
                    }
                    lock (apiHelpersTypes)
                    {
                        apiHelpersTypes.Add(_eventToHandle.RequestService, apiHelperTypeForDictionary);
                    }
                }
                // TODO Separe void CallApiService
                Type apiHelperType;
                lock (apiHelpersTypes)
                {
                    apiHelperType = apiHelpersTypes[_eventToHandle.RequestService];
                }
                GenericAPIHelper genericAPIHelper = ModulesHelper.GetInstance<GenericAPIHelper>(apiHelperType);
                // TODO Check exist
                string serviceTypeString = OracleContext.GetRequestedType(_eventToHandle.RequestService, (int)_eventToHandle.RequestServiceType).ServiceTypeString;

                string wantedValue = genericAPIHelper.GetWantedValue(serviceTypeString);
                var receipt = _service.SendResponseRequestAndWaitForReceiptAsync(
                        clientAddress: _eventToHandle.Sender,
                        service: _eventToHandle.RequestService,
                        serviceType: _eventToHandle.RequestServiceType,
                        value: wantedValue
                        );
                receipt.Wait();

                if (receipt.Result.Status.Value == 1)
                {
                    StringWriter.Enqueue(message + " Successfull request handled for service: " + _eventToHandle.RequestService + " for type: " + serviceTypeString + " From: " + _eventToHandle.Sender + " Result: " + wantedValue);
                }
                else
                {
                    throw new Exception("Failed to send result on blockchain (Re-Enqueued...). From: " + _eventToHandle.Sender + " Service: " + _eventToHandle.RequestService + " ServiceType: " + serviceTypeString);
                }
            }
            catch(Exception e)
            {
                EnqueueEvent(_eventToHandle);
                StringWriter.Enqueue(message+ " " + e.Message);
            }
            finally
            {
                EnqueueService(_service,_services);
            }
        }

        private List<RequestEventEventDTO> GetList()
        {
            List<RequestEventEventDTO> list = null;
            if (queueEventsToHandle.Count > 0)
            { 
                lock (queueEventsToHandle)
                {
                    list = new List<RequestEventEventDTO>(queueEventsToHandle);
                    queueEventsToHandle.Clear();
                }
            }
            return list;
        }

        private void EnqueueService(Oracle888730Service _service, Queue<Oracle888730Service> _services)
        {
            lock (_services)
            {
                _services.Enqueue(_service);
            }
        }
        private Oracle888730Service DequeueService(Queue<Oracle888730Service> _services)
        {
            Oracle888730Service service = null;
            while (service == null)
            {
                if (_services.Count > 0)
                {
                    lock (_services)
                    {
                        service = _services.Dequeue();
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
            return service;
        }

        public static void EnqueueEvent(RequestEventEventDTO _event)
        {
            lock (queueEventsToHandle)
            {
                queueEventsToHandle.Enqueue(_event);
            }
        }
        public static void EnqueueEvent(List<RequestEventEventDTO> _events)
        {
            lock (queueEventsToHandle)
            {
                _events.ForEach(x => {
                    queueEventsToHandle.Enqueue(x);
                });
            }
        }
    }
}
