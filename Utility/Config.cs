using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using System.Runtime.CompilerServices;

namespace Oracle888730.Utility
{
    public class Config
    {
        private static string fileName = Directory.GetCurrentDirectory() + "\\config.json";

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
            string jsonString;
            Config c = new Config();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("[WARNING] Missing config.json file...");
                jsonString = JsonSerializer.Serialize<Config>(c);
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine("[WARNING] File template created. Fill it with your setup settings.");
                Exit();
                return c;
            }
            Console.WriteLine("[PROGRAM] Retrieving config datas...");
            jsonString = File.ReadAllText(fileName);
            try
            {
                c = JsonSerializer.Deserialize<Config>(jsonString);
                return c;
            }
            catch
            {
                Console.WriteLine("[ERROR] Couldn't convert config.json file...");
                Exit();
                return c;
            }
        }

        public static void Exit()
        {
            Console.WriteLine("[PROGRAM] Press any key to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void Save()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize<Config>(this);
                File.WriteAllText(fileName, jsonString);
            }
            catch 
            {
                Console.WriteLine("[ERROR] Impossible to save config datas.");
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
