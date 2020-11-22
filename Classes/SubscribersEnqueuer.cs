using Nethereum.RPC.Shh.DTOs;
using Oracle888730.Classes.Handlers;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using Oracle888730.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Oracle888730.Classes
{
    class SubscribersEnqueuer : IGenericListener
    {
        private string message;
        protected Dictionary<string, Type> handlers;
        private string handlersNameSpace;
        
        public SubscribersEnqueuer()
        {
            message = "[SubscribeEnqueuer]";
            handlers = new Dictionary<string, Type>();
            handlersNameSpace = "Classes.Handlers";
        }
        public Thread Start()
        {
            Thread threadListener = new Thread(Enqueuer);
            threadListener.Start();
            return threadListener;
        }

        private void Enqueuer()
        {
            try
            {
                Thread.Sleep(2000);
                Stopwatch timer = new Stopwatch();
                while (true)
                {
                    timer.Start();
                    List<Subscriber> subscribers = OracleContext.GetSubscribers();
                    subscribers.ForEach(x =>
                    {
                        /*
                        string service = x.ServiceType.Service.ServiceName.ToUpper();
                        Type type;
                        if (handlers.ContainsKey(service))
                        {
                            type = handlers.GetValueOrDefault(service);
                        }
                        else
                        {
                            type = ModulesHelper.GetType(service, handlersNameSpace);
                            handlers.Add(service, type);
                        }
                        if (type != null)
                        {
                            EnqueueSubscriberRequest(type, x);
                        }*/
                        RequestEventEventDTO requestEvent = new RequestEventEventDTO();
                        requestEvent.Sender = x.Address;
                        requestEvent.RequestService = x.ServiceType.Service.ServiceName;
                        requestEvent.RequestServiceType = x.ServiceTypeForeignKey;
                        MainHandler.EnqueueEvent(requestEvent);
                    });
                    object temp = new object();
                    timer.Stop();
                    lock (temp)
                    {
                        Monitor.Wait(temp, 3600000 - (int)timer.Elapsed.TotalMilliseconds);
                    }
                    timer.Reset();
                }
            }
            catch(Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR] " + e.Message);
                Enqueuer();
            }
        }

       /* private static void EnqueueSubscriberRequest(Type _type, Subscriber _request)
        {
            RequestEventEventDTO requestEvent = new RequestEventEventDTO();
            requestEvent.Sender = _request.Address;
            requestEvent.RequestService = _request.ServiceType.Service.ServiceName;
            requestEvent.RequestServiceType = _request.ServiceTypeForeignKey;
            //MethodInfo staticMethodInfo = _type.GetMethod("Enqueue");
            //staticMethodInfo.Invoke(null, new object[] { _request });
            //GenericHandler currentType = ModulesHelper.GetInstance<GenericHandler>(type, new object[] { web3, config, message});
            //currentType.Enqueue(x.Event);
        }*/
    }
}
