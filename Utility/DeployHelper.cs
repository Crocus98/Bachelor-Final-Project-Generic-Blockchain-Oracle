﻿using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Oracle888730.Classes;
using System.Threading;
using System.Linq;

namespace Oracle888730.Utility
{
    class DeployHelper
    {
        
        public static Web3 web3;
        public static Oracle888730Service contractService;
        private readonly Config config;
        private readonly List<string> nameSpaces = new List<string>{
                "Classes.Listeners",
                "Classes.Handlers"
            };
        private Account account;
        private readonly string message = "[DeployHelper]";

        public DeployHelper(Config _config) {
            config = _config;
        }

        //Avvia l'oracolo secondo la procedura adatta
        public void ConnectOrDeploy() {
            var contractAddress = config.Oracle.ContractAddress;
            if (contractAddress != null && contractAddress.Length >= 40)
            {
                ConnectAsync().Wait();
            }
            else
            {
                DeployAsync().Wait();
            }
        }

        //Chiama i metodi che avviano i thread necessari al oracolo per funzionare
        public void StartOracle()
        {
            List<Thread> threadList = new List<Thread>();
            StartListenersAndHandler(threadList);
            StartSubscriber(threadList);
            threadList.ForEach(x =>
            {
                x.Join();
            });
        }

        private void StartSubscriber(List<Thread> threadList)
        {
            threadList.Add(new SubscribersEnqueuer().Start());
        }

        private void StartListenersAndHandler(List<Thread> threadList)
        {
            nameSpaces.ForEach(x => {
                List<Type> types = ModulesHelper.GetTypes(x);
                types.ForEach(z => {
                    dynamic current = ModulesHelper.GetInstance<dynamic>(z, new object[] { web3, config });
                    threadList.Add(
                        ((IGeneric)current).Start()
                        );
                    current = null;
                });
            });
        }
        //Effettua il deploy del contratto se non già presente su blockchain
        private async Task DeployAsync()
        {
            try
            {
                SetAbi();
                SetWeb3();
                var oracle888730Deployment = new Oracle888730Deployment();
                StringWriter.Enqueue(message + "[PROGRAM] Deploying the smart contract on blockchain..."); 
                var transactionReceiptDeployment = await web3.Eth.GetContractDeploymentHandler<Oracle888730Deployment>().SendRequestAndWaitForReceiptAsync(oracle888730Deployment);
                var contractAddress = transactionReceiptDeployment.ContractAddress;
                config.Oracle.ContractAddress = contractAddress;
                config.Save();
                StringWriter.Enqueue(message + "[PROGRAM] Creating contract service...");
                contractService = new Oracle888730Service(web3, contractAddress);
                var contractName = await contractService.OracleNameQueryAsync();
                StringWriter.Enqueue(message + "[PROGRAM] Contract " + contractName + " created at address: " + contractAddress);
                if (config.RpcServer.SecondaryAddresses != null)
                {
                    config.RpcServer.SecondaryAddresses.ToList().ForEach(x =>
                    {
                        var receipt = contractService.AddOracleSecondaryAccountRequestAndWaitForReceiptAsync(x[0]);
                        receipt.Wait();
                        if(receipt.Result.Status.Value == 0)
                        {
                            //TODO Msg
                            StringWriter.Enqueue(message + "[ERR]");
                            Config.Exit();
                        }
                        else
                        {
                            StringWriter.Enqueue(message + "[PROGRAM] Oracle secondary address added to new contract.");
                        }
                    });
                }
                else
                {
                    StringWriter.Enqueue(message + "[PROGRAM] No secondary Addresses");
                }
                
            }
            catch (Exception)
            {
                StringWriter.Enqueue(message + "[ERR] Couldn't deploy the contract.");
                StringWriter.Enqueue(message + "[ADVISE] Check that ur RPC client is running.");
                Config.Exit();
            }
        }

        //Connette l'applicazione al contratto su blockchcain
        private async Task ConnectAsync()
        {
            try
            {
                SetAbi();
                SetWeb3();
                var contractAddress = config.Oracle.ContractAddress;
                StringWriter.Enqueue(message + " Creating contract service...");
                contractService = new Oracle888730Service(web3, contractAddress);
                var contractName = await contractService.OracleNameQueryAsync();
                StringWriter.Enqueue(message + " Connecting to contract " + contractName + " with address: " + contractAddress);
            }
            catch (Exception)
            {
                StringWriter.Enqueue(message + "[ERR] Couldn't connect to contract.");
                StringWriter.Enqueue(message + "[ADVISE] Check that ur RPC client is running and check \nthat the contract address in your config.json file is correct. ");
                Config.Exit();
            }
        }

        //Imposta il Web3 dell'account principale
        private void SetWeb3()
        {
            var url = config.RpcServer.Url;
            var privateKey = config.RpcServer.PrivateKey;
            account = new Account(privateKey);
            web3 = new Web3(account, url);
        }

        //Imposta l'Abi del contratto
        private void SetAbi()
        {
            if (config.Oracle.Abi == null || config.Oracle.Abi == "")
            {
                string path = Directory.GetParent(
                    Directory.GetParent(
                        Directory.GetParent(
                            Directory.GetCurrentDirectory()
                            ).ToString()
                        ).ToString()
                    ) + @"\bin\Solidity\Oracle888730.abi";
                if (!File.Exists(path))
                {
                    StringWriter.Enqueue(message + "[ERR] Missing Abi file");
                    StringWriter.Enqueue(message + "[ADVISE] Compile your Solidity contract before continuing.");
                    Config.Exit();
                }
                config.Oracle.Abi = File.ReadAllText(path);
                config.Save();
            }
        }


    }
}
