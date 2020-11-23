using System;
using System.Linq;
using System.Threading.Tasks;
using Oracle888730.OracleEF;
using Oracle888730.Utility;

namespace Oracle888730
{
    class Program
    {
        public static Config config;
        static void Main()
        {
            Console.WriteLine("[PROGRAM] Oracle888730 starting...");
            StringWriter stringWriter = new StringWriter();
            SetupDb();
            config = Config.Load();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomainProcessExit);
            DeployHelper deployHelper = new DeployHelper(config);
            deployHelper.ConnectOrDeploy();
            deployHelper.StartOracle();
        }

        static void CurrentDomainProcessExit(object sender, EventArgs e)
        {
            config.Save();
        }

        static void SetupDb()
        {
            try
            {
                OracleContext db = new OracleContext();
                db.Database.EnsureCreated();
            }
            catch
            {
                Console.WriteLine("[PROGRAM][ERROR] Impossible starting database...");
                Config.Exit();
            }
        }
    }
}
