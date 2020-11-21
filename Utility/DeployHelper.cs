using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Oracle888730.Contracts.Oracle888730;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Oracle888730.Classes;
using Flurl.Util;
using System.Threading;
using Oracle888730.Classes.Listeners;
using Oracle888730.Classes.Handlers;
using System.Reflection;
using Nethereum.Web3.Accounts;
using Nethereum.RPC.NonceServices;

namespace Oracle888730.Utility
{
    class DeployHelper
    {
        
        public static Web3 web3;
        public static Oracle888730Service contractService;
        private Config config;
        private string listenersNameSpace;
        private InMemoryNonceService inMemoryNonceService;
        private Account account;

        public DeployHelper(Config _config) {
            config = _config;
            listenersNameSpace = "Classes.Listeners";
        } 

        public void ConnectOrDeploy() {
            var contractAddress = config.Oracle.ContractAddress;
            if (contractAddress != null && contractAddress.Length >= 40)
            {
                ConnectAsync(config).Wait();
            }
            else
            {
                DeployAsync(config).Wait();
            }

        }

        public void StartListener()
        {
            List<Thread> threadList = new List<Thread>();
            List<Type> listeners = ModulesHelper.GetTypes(listenersNameSpace);
            inMemoryNonceService = new InMemoryNonceService(config.RpcServer.PublicKey, web3.Client);
            listeners.ForEach(x =>
            {
                GenericListener current = ModulesHelper.GetInstance<GenericListener>(x, new object[] {web3, config, account, inMemoryNonceService });
                threadList.Add(current.Start());
            });
            threadList.ForEach(x =>
            {
                x.Join();
            });
        }

        private async Task DeployAsync(Config _config)
        {
            try
            {
                SetAbi(_config);
                SetWeb3(_config);
                var oracle888730Deployment = new Oracle888730Deployment();
                Console.WriteLine("[PROGRAM] Deploying the smart contract on blockchain..."); 
                var transactionReceiptDeployment = await web3.Eth.GetContractDeploymentHandler<Oracle888730Deployment>().SendRequestAndWaitForReceiptAsync(oracle888730Deployment);
                var contractAddress = transactionReceiptDeployment.ContractAddress;
                _config.Oracle.ContractAddress = contractAddress;
                _config.Save();
                Console.WriteLine("[PROGRAM] Creating contract service...");
                contractService = new Oracle888730Service(web3, contractAddress);
                var contractName = await contractService.OracleNameQueryAsync();
                Console.WriteLine("[PROGRAM] Contract " + contractName + " created at address: " + contractAddress);
            }
            catch
            {
                Console.WriteLine("[ERR] Couldn't deploy the contract.");
                Console.WriteLine("[ADVISE] Check that ur RPC client is running.");
                Config.Exit();
            }
        }

        private async Task ConnectAsync(Config _config)
        {
            try
            {
                SetAbi(_config);
                SetWeb3(_config);
                var contractAddress = _config.Oracle.ContractAddress;
                Console.WriteLine("[PROGRAM] Creating contract service...");
                contractService = new Oracle888730Service(web3, contractAddress);
                var contractName = await contractService.OracleNameQueryAsync();
                Console.WriteLine("[PROGRAM] Connecting to contract " + contractName + " with address: " + contractAddress);
            }
            catch
            {
                Console.WriteLine("[ERR] Couldn't connect to contract.");
                Console.WriteLine("[ADVISE] Check that ur RPC client is running and check \nthat the contract address in your config.json file is correct. ");
                Config.Exit();
            }
        }
        private void SetWeb3(Config _config)
        {
            var url = _config.RpcServer.Url;
            var privateKey = _config.RpcServer.PrivateKey;
            account = new Nethereum.Web3.Accounts.Account(privateKey);
            web3 = new Web3(account, url);
        }

        private void SetAbi(Config _config)
        {
            if (_config.Oracle.Abi == null || _config.Oracle.Abi == "")
            {
                string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()) + @"\bin\Solidity\Oracle888730.abi";
                if (!File.Exists(path))
                {
                    Console.WriteLine("[ERR] Missing Abi file");
                    Console.WriteLine("[ADVISE] Compile your Solidity contract before continuing.");
                    Config.Exit();
                }
                _config.Oracle.Abi = File.ReadAllText(path);
                _config.Save();
            }
        }


    }
}
