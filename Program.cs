using System;
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
            //Avviamento del servizio che gestisce la console
            StringWriter stringWriter = new StringWriter();
            SetupDb();
            //Caricamento dei dati dal file di config
            config = Config.Load();
            //Aggiunta event handler per salvataggio del file di config all'uscita del programma
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomainProcessExit);
            //Classe per la connessione ad un contratto o in sua mancanza per la sua creazione
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
