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
        private readonly string message = "[SubscribeEnqueuer]";

        public SubscribersEnqueuer()
        {
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
                            RequestEventEventDTO requestEvent = new RequestEventEventDTO
                            {
                                Sender = x.Address,
                                RequestService = x.ServiceType.Service.ServiceName,
                                RequestServiceType = x.ServiceTypeForeignKey
                            };
                            MainHandler.EnqueueEvent(requestEvent);
                        });
                        object temp = new object();
                        timer.Stop();
                        lock (temp)
                        {
                            Monitor.Wait(temp, 300000 - (int)timer.Elapsed.TotalMilliseconds);
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
