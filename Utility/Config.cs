using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Oracle888730.Utility
{
    public class Config
    {
        private static readonly string fileName = Directory.GetCurrentDirectory() + "\\config.json";
        private static readonly string message = "[CONFIG]";

        [JsonPropertyName("rpc_server")]
        public RpcServer RpcServer { get; set; }

        [JsonPropertyName("oracle")]
        public Oracle Oracle { get; set; }

        [JsonPropertyName("db")]
        public Db Db { get; set; }

        public Config()
        {
            this.RpcServer = new RpcServer();
            this.Oracle = new Oracle();
            this.Db = new Db();
        }
        public static Config Load()
        {
            CheckExist();
            Config config = new Config();
            StringWriter.Enqueue(message + " Retrieving config datas...");
            string jsonString = File.ReadAllText(fileName);
            try
            {
                config = JsonSerializer.Deserialize<Config>(jsonString);
            }
            catch
            {
                StringWriter.Enqueue(message + "[ERROR] Couldn't convert config.json file...");
                Exit();
                
            }
            return config;
        }

        private static void CheckExist()
        {
            if (!File.Exists(fileName))
            {
                StringWriter.Enqueue(message + "[ERROR] Missing config.json file...");
                string jsonString = JsonSerializer.Serialize<Config>(new Config());
                File.WriteAllText(fileName, jsonString);
                StringWriter.Enqueue(message + "[WARNING] File template created. Fill it with your setup settings.");
                Exit();
            }
        }

        public static void Exit()
        {
            StringWriter.Enqueue(message + " Press any key to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void Save()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(this);
                File.WriteAllText(fileName, jsonString);
            }
            catch 
            {
                StringWriter.Enqueue(message + "[ERROR] Impossible to save config datas.");
                Exit();
            }
        }
    }

    public class RpcServer
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; set; }

        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        [JsonPropertyName("secondaryAdresses")]
        public IList<IList<string>> SecondaryAddresses { get; set; }
    }

    public class Oracle
    {
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; }

        [JsonPropertyName("abi")]
        public string Abi { get; set; }

        [JsonPropertyName("latestBlock")]
        public string LatestBlock { get; set; }
    }

    public class Db
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("pass")]
        public string Pass { get; set; }

        [JsonPropertyName("dbnm")]
        public string Dbnm { get; set; }
    }
}
