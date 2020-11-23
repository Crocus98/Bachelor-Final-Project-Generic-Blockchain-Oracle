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
    class SubscribersEnqueuer : IGeneric
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
                StringWriter.Enqueue(message + " Subscriber enqueuer setup started...");
                Thread.Sleep(2000);
                Stopwatch timer = new Stopwatch();
                StringWriter.Enqueue(message + " Subscriber enqueuer started");
                while (true)
                {
                    timer.Start();
                    List<Subscriber> subscribers = OracleContext.GetSubscribers();
                    if (subscribers != null && subscribers.Count > 0)
                    {
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
                    }
                    else
                    {
                        timer.Stop();
                        Thread.Sleep(1000);
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
    }
}
