using Nethereum.Web3;
using Oracle888730.Utility;
using System;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Oracle888730.Contracts.Oracle888730;
using Nethereum.Web3.Accounts;

namespace Oracle888730.Classes.Handlers
{
    class MainHandler : GenericHandler
    {
        protected static Queue<RequestEventEventDTO> queueEventsToHandle;
        protected Dictionary<string, Type> apiHelpersTypes;
        protected string apiHelpersNamespace = "Utility.ApiHelpers";
        private static readonly object syncObject = new object();

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
                Queue<Oracle888730Service> services = SetupHandlerAndGetServices();
                while (true)
                {
                    tempEventList = GetList();
                    if(tempEventList == null)
                    {
                        Thread.Sleep(500);
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

        private Queue<Oracle888730Service> SetupHandlerAndGetServices()
        {
            StringWriter.Enqueue(message + " Handler setup started...");
            Queue<Oracle888730Service> services = new Queue<Oracle888730Service>();
            //services.Enqueue(contractService);
            config.RpcServer.SecondaryAddresses.ToList().ForEach(x =>
            {
                Account secondaryAccount = new Account(x[1]);
                Web3 secondaryAccountWeb3 = new Web3(secondaryAccount, config.RpcServer.Url);
                services.Enqueue(new Oracle888730Service(secondaryAccountWeb3, config.Oracle.ContractAddress));
            });
            ThreadPool.SetMinThreads(0, 0);
            ThreadPool.SetMaxThreads(services.Count(), 0);
            StringWriter.Enqueue(message + " Handler started with " + services.Count() + " threads");
            return services;
        }

        private void HandleMethod(RequestEventEventDTO _eventToHandle, Oracle888730Service _service, Queue<Oracle888730Service> _services)
        {
            try
            {
                CheckApiDictionary(_eventToHandle);
                string wantedValue = GetValueFromApi(_eventToHandle);
                SendTransaction(_eventToHandle,_service, wantedValue );
            }
            catch(Exception e)
            {
                StringWriter.Enqueue(message+ "[ERROR] " + e.Message);
            }
            finally
            {
                EnqueueService(_service,_services);
            }
        }

        private void SendTransaction(RequestEventEventDTO _eventToHandle,Oracle888730Service _service, string _wantedValue)
        {
            try
            {
                var receipt = _service.SendResponseRequestAndWaitForReceiptAsync(
                    clientAddress: _eventToHandle.Sender,
                    service: _eventToHandle.RequestService,
                    serviceType: _eventToHandle.RequestServiceType,
                    value: _wantedValue
                    );
                receipt.Wait();
                if (receipt.Result.Status.Value == 1)
                {
                    StringWriter.Enqueue(message + " Successfull request handled for service: " + _eventToHandle.RequestService + " for type: " + _eventToHandle.RequestServiceType + " From: " + _eventToHandle.Sender + " Result: " + _wantedValue);
                }
            }
            catch (Exception e)
            {
                lock (syncObject)
                {
                    Monitor.Wait(syncObject,500);
                }
                EnqueueEvent(_eventToHandle);
                throw new Exception("Failed to send result on blockchain " +e.Message +" (Re-Enqueued...). From: " + _eventToHandle.Sender + " Service: " + _eventToHandle.RequestService + " ServiceType: " + _eventToHandle.RequestServiceType + " Thread: " + Thread.CurrentThread.ManagedThreadId);
            }
        }
        private void CheckApiDictionary(RequestEventEventDTO _eventToHandle)
        {
            if (!CheckDictionaryContainsKey(_eventToHandle.RequestService))
            {
                try
                {
                    Type apiHelperTypeForDictionary = ModulesHelper.GetType(_eventToHandle.RequestService, apiHelpersNamespace);
                    if (apiHelperTypeForDictionary == null)
                    {
                        throw new Exception();
                    }
                    AddElementToDictionary(_eventToHandle.RequestService, apiHelperTypeForDictionary);
                }
                catch
                {
                    throw new Exception("There is no service like: " + _eventToHandle.RequestService + " Asked from: " + _eventToHandle.Sender);
                }
            }
        }

        private string GetValueFromApi(RequestEventEventDTO _eventToHandle)
        {
            GenericAPIHelper genericAPIHelper;
            lock (apiHelpersTypes)
            {
                genericAPIHelper = ModulesHelper
                    .GetInstance<GenericAPIHelper>(
                        apiHelpersTypes[_eventToHandle.RequestService]
                    );
            }
            ServiceType serviceType = OracleContext.GetRequestedType(
                    _eventToHandle.RequestService,
                    (int)_eventToHandle.RequestServiceType
                );
            string serviceTypeString;
            if (serviceType != null)
            {
                serviceTypeString = serviceType.ServiceTypeString;
            }
            else
            {
                throw new Exception("Failed request for non existent service type " + _eventToHandle.RequestServiceType + " of service "+ _eventToHandle.RequestService+ ". From address: " + _eventToHandle.Sender);
            }
            return genericAPIHelper.GetWantedValue(serviceTypeString);
        }

        private void AddElementToDictionary(string _serviceName, Type _type)
        {
            lock (apiHelpersTypes)
            {
                if (!CheckDictionaryContainsKey(_serviceName))
                {
                    apiHelpersTypes.Add(_serviceName, _type);
                }
            }
        }

        private bool CheckDictionaryContainsKey(string _serviceName)
        {
            lock (apiHelpersTypes)
            {
                return apiHelpersTypes.ContainsKey(_serviceName);
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
        private  Oracle888730Service DequeueService(Queue<Oracle888730Service> _services)
        {
            lock (syncObject)
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
                        Thread.Sleep(10);
                    }
                }
                return service;
            }
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
