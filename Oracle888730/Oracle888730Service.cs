using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;

namespace Oracle888730.Contracts.Oracle888730
{
    public partial class Oracle888730Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, Oracle888730Deployment oracle888730Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<Oracle888730Deployment>().SendRequestAndWaitForReceiptAsync(oracle888730Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, Oracle888730Deployment oracle888730Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<Oracle888730Deployment>().SendRequestAsync(oracle888730Deployment);
        }

        public static async Task<Oracle888730Service> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, Oracle888730Deployment oracle888730Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, oracle888730Deployment, cancellationTokenSource);
            return new Oracle888730Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public Oracle888730Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddOracleSecondaryAccountRequestAsync(AddOracleSecondaryAccountFunction addOracleSecondaryAccountFunction)
        {
             return ContractHandler.SendRequestAsync(addOracleSecondaryAccountFunction);
        }

        public Task<TransactionReceipt> AddOracleSecondaryAccountRequestAndWaitForReceiptAsync(AddOracleSecondaryAccountFunction addOracleSecondaryAccountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addOracleSecondaryAccountFunction, cancellationToken);
        }

        public Task<string> AddOracleSecondaryAccountRequestAsync(string newOracleSecondaryAccount)
        {
            var addOracleSecondaryAccountFunction = new AddOracleSecondaryAccountFunction();
                addOracleSecondaryAccountFunction.NewOracleSecondaryAccount = newOracleSecondaryAccount;
            
             return ContractHandler.SendRequestAsync(addOracleSecondaryAccountFunction);
        }

        public Task<TransactionReceipt> AddOracleSecondaryAccountRequestAndWaitForReceiptAsync(string newOracleSecondaryAccount, CancellationTokenSource cancellationToken = null)
        {
            var addOracleSecondaryAccountFunction = new AddOracleSecondaryAccountFunction();
                addOracleSecondaryAccountFunction.NewOracleSecondaryAccount = newOracleSecondaryAccount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addOracleSecondaryAccountFunction, cancellationToken);
        }

        public Task<string> GetRequestRequestAsync(GetRequestFunction getRequestFunction)
        {
             return ContractHandler.SendRequestAsync(getRequestFunction);
        }

        public Task<TransactionReceipt> GetRequestRequestAndWaitForReceiptAsync(GetRequestFunction getRequestFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getRequestFunction, cancellationToken);
        }

        public Task<string> GetRequestRequestAsync(string service, BigInteger serviceType)
        {
            var getRequestFunction = new GetRequestFunction();
                getRequestFunction.Service = service;
                getRequestFunction.ServiceType = serviceType;
            
             return ContractHandler.SendRequestAsync(getRequestFunction);
        }

        public Task<TransactionReceipt> GetRequestRequestAndWaitForReceiptAsync(string service, BigInteger serviceType, CancellationTokenSource cancellationToken = null)
        {
            var getRequestFunction = new GetRequestFunction();
                getRequestFunction.Service = service;
                getRequestFunction.ServiceType = serviceType;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getRequestFunction, cancellationToken);
        }

        public Task<string> GetSubscribeRequestRequestAsync(GetSubscribeRequestFunction getSubscribeRequestFunction)
        {
             return ContractHandler.SendRequestAsync(getSubscribeRequestFunction);
        }

        public Task<TransactionReceipt> GetSubscribeRequestRequestAndWaitForReceiptAsync(GetSubscribeRequestFunction getSubscribeRequestFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getSubscribeRequestFunction, cancellationToken);
        }

        public Task<string> GetSubscribeRequestRequestAsync(string service, BigInteger serviceType)
        {
            var getSubscribeRequestFunction = new GetSubscribeRequestFunction();
                getSubscribeRequestFunction.Service = service;
                getSubscribeRequestFunction.ServiceType = serviceType;
            
             return ContractHandler.SendRequestAsync(getSubscribeRequestFunction);
        }

        public Task<TransactionReceipt> GetSubscribeRequestRequestAndWaitForReceiptAsync(string service, BigInteger serviceType, CancellationTokenSource cancellationToken = null)
        {
            var getSubscribeRequestFunction = new GetSubscribeRequestFunction();
                getSubscribeRequestFunction.Service = service;
                getSubscribeRequestFunction.ServiceType = serviceType;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getSubscribeRequestFunction, cancellationToken);
        }

        public Task<string> OracleNameQueryAsync(OracleNameFunction oracleNameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OracleNameFunction, string>(oracleNameFunction, blockParameter);
        }

        
        public Task<string> OracleNameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OracleNameFunction, string>(null, blockParameter);
        }

        public Task<string> PayServiceRequestAsync(PayServiceFunction payServiceFunction)
        {
             return ContractHandler.SendRequestAsync(payServiceFunction);
        }

        public Task<TransactionReceipt> PayServiceRequestAndWaitForReceiptAsync(PayServiceFunction payServiceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(payServiceFunction, cancellationToken);
        }

        public Task<string> PayServiceRequestAsync(string service)
        {
            var payServiceFunction = new PayServiceFunction();
                payServiceFunction.Service = service;
            
             return ContractHandler.SendRequestAsync(payServiceFunction);
        }

        public Task<TransactionReceipt> PayServiceRequestAndWaitForReceiptAsync(string service, CancellationTokenSource cancellationToken = null)
        {
            var payServiceFunction = new PayServiceFunction();
                payServiceFunction.Service = service;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(payServiceFunction, cancellationToken);
        }

        public Task<string> SendResponseRequestAsync(SendResponseFunction sendResponseFunction)
        {
             return ContractHandler.SendRequestAsync(sendResponseFunction);
        }

        public Task<TransactionReceipt> SendResponseRequestAndWaitForReceiptAsync(SendResponseFunction sendResponseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(sendResponseFunction, cancellationToken);
        }

        public Task<string> SendResponseRequestAsync(string clientAddress, string service, BigInteger serviceType, string value)
        {
            var sendResponseFunction = new SendResponseFunction();
                sendResponseFunction.ClientAddress = clientAddress;
                sendResponseFunction.Service = service;
                sendResponseFunction.ServiceType = serviceType;
                sendResponseFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(sendResponseFunction);
        }

        public Task<TransactionReceipt> SendResponseRequestAndWaitForReceiptAsync(string clientAddress, string service, BigInteger serviceType, string value, CancellationTokenSource cancellationToken = null)
        {
            var sendResponseFunction = new SendResponseFunction();
                sendResponseFunction.ClientAddress = clientAddress;
                sendResponseFunction.Service = service;
                sendResponseFunction.ServiceType = serviceType;
                sendResponseFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(sendResponseFunction, cancellationToken);
        }

        public Task<string> OracleOwnerQueryAsync(OracleOwnerFunction oracleOwnerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OracleOwnerFunction, string>(oracleOwnerFunction, blockParameter);
        }

        
        public Task<string> OracleOwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OracleOwnerFunction, string>(null, blockParameter);
        }
    }
}
