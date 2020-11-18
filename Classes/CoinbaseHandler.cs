using Microsoft.EntityFrameworkCore;
using Nethereum.Contracts;
using Nethereum.Web3;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF;
using Oracle888730.OracleEF.Models;
using Oracle888730.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oracle888730.Classes
{
    class CoinbaseHandler : GenericHandler<SubscribeEventEventDTO>
    {
        private new CallAPIHelper callApiHelper;
        private Stopwatch stopWatch;

        public CoinbaseHandler(Web3 _web3, Config _config) : base (_web3, _config)
        {
            callApiHelper = new CallAPIHelper();
            stopWatch = new Stopwatch();
            message = "[CoinbaseHandler]";
        }

        //Mando i risultati una volta all'ora
        protected override void Handler()
        {
            //Messo per vedere il risultato
            Thread.Sleep(2000);
            while (true)
            {
                /*stopWatch.Start();
                List<CurrencyChangesEnum.CurrencyChanges> changes = Enum.GetValues(typeof(CurrencyChangesEnum.CurrencyChanges)).Cast<CurrencyChangesEnum.CurrencyChanges>().ToList();
                changes.ForEach(x => {
                    List<Subscriber> temporaryList = new List<Subscriber>();
                    using (var db = new OracleContext())
                    {
                        temporaryList.AddRange(db.GetListFromSubscriptionType((int)x));
                    }
                    if(temporaryList.Count > 0)
                    {
                        temporaryList.ForEach(y => {
                            HandleSingleRequest(y);
                        });
                        StringWriter.Enqueue(message + " Successfully satisfied subscribers service requests for service: " + (int)x);
                    }
                    else
                    {
                        StringWriter.Enqueue(message+" There are currently no subscribers for service: " + (int)x);
                    }
                });

                object waitTimeOut = new object();
                stopWatch.Stop();
                lock (waitTimeOut)
                {
                    Monitor.Wait(waitTimeOut, TimeSpan.FromMilliseconds((3600 * 1000)- stopWatch.Elapsed.TotalMilliseconds));
                }*/
            }
        }

        protected void HandleSingleRequest(Subscriber _subscriber)
        {

           /* //Da fare
            string service = _subscriber.Service;
            int serviceType = _subscriber.ServiceType;
            string address = _subscriber.Address;
            var handler = ModulesHelper.GetInstance(service);
            if(handler == null)
            {
                return;
            }
            
            //Using the same instance of Web3 across services, there is a nonce manager (in memory) that will ensure that your transactions are in the right order (safe thread too)
            var res = contractService.SendResponseRequestAndWaitForReceiptAsync(
                clientAddress: address,
                value: value.Result,
                service: service,
                serviceType: serviceType
            );*/
        }
    }
}
