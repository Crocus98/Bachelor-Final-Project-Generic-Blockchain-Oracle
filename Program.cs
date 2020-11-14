using System;
using System.Threading.Tasks;
using Oracle888730.OracleEF;
using Oracle888730.Utility;

namespace Oracle888730
{
    class Program
    {
        public static Config config;
        public static OracleContext db;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomainProcessExit);
            config = Config.Load();
            SetupDb();
            StringWriter sw = new StringWriter();
            DeployHelper deployHelper = new DeployHelper(config);
            deployHelper.ConnectOrDeploy();
            deployHelper.StartListener();
        }

        static void CurrentDomainProcessExit(object sender, EventArgs e)
        {
            config.Save();
        }

        static void SetupDb()
        {
            try
            {
                db = new OracleContext();
                db.Database.EnsureCreated();
            }
            catch
            {
                Console.WriteLine("[ERROR] Impossible starting database...");
                Config.Exit();
            }
        }
    }
}
