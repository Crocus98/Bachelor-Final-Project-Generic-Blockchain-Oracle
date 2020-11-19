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

namespace Oracle888730.Classes.Handlers
{
    class COINBASEHandler : GenericHandler
    {

        public COINBASEHandler(Web3 _web3, Config _config, string _callerMessage) : base (_web3, _config)
        {
            message = _callerMessage+"[CoinbaseHandler]";
            apiHelper = new COINBASEAPIHelper();
        }

        protected override void Handler(RequestEventEventDTO _event)
        {
            try
            {
                // Assegni Valori
                string requestService = _event.RequestService;
                int requestServiceType = (int)_event.RequestServiceType;
                string senderAddress = _event.Sender;

                // Controllo se il servizio esiste in DB
                ServiceType serviceType = OracleContext.GetRequestedType(requestService, requestServiceType);
                if (serviceType == null)
                {
                    StringWriter.Enqueue(message + " Failed request for inexistent service type: " + requestServiceType + " From: " + senderAddress);
                    return;
                }

                // Se Esiste mandi richiesta a Coinbase
                var coinbaseRequest = new COINBASEAPIHelper().GetWantedValue(serviceType.ServiceTypeString);
                coinbaseRequest.Wait();
                string requestedValue = coinbaseRequest.Result;
                Task<TransactionReceipt> receipt = null;
                int i = 0;
                do
                {
                    try
                    {
                        receipt = contractService.SendResponseRequestAndWaitForReceiptAsync(
                                clientAddress: senderAddress,
                                service: requestService,
                                serviceType: requestServiceType,
                                value: requestedValue
                            );
                        i++;
                        receipt.Wait();
                    }
                    catch(Exception e)
                    {
                        StringWriter.Enqueue(message + "First attempt failed. Retrying...");
                    }
                } while (i < 3 && receipt.Result.Status.Value == 0);
                // Esito risposta
                if(receipt.Result.Status.Value == 1)
                {
                    StringWriter.Enqueue(message + " Successfull request handled for service: " + requestService + " for type: " +requestServiceType +" From: "+ senderAddress + " Result: " + requestedValue);
                }
                else
                {
                    throw new Exception("Failed to send result on blockchain after 3 attempts. From: " + senderAddress + " Service: " + requestService + " ServiceType: " + requestServiceType);
                }
            }
            catch (Exception e)
            {
                StringWriter.Enqueue(message + "[ERROR] " + e.Message);
            }
        }
    }
}
