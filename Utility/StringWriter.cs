using Nethereum.RPC.Shh.DTOs;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oracle888730.Utility
{
    class StringWriter
    {
        private static Queue<string> stringsToBeWritten;
        private readonly string message = "[StringWriter]";

        public StringWriter()
        {
            stringsToBeWritten = new Queue<string>();
            Start();
            Enqueue(message + " Service started successfully...");
        }

        public void Start()
        {
            Thread writeMessages = new Thread(DoWork);
            writeMessages.Start();
        }

        //Controlla l'accesso concorrente alla coda di stringhe da stampare
        public static void Enqueue(string _stringToEnqueue)
        {
            lock (stringsToBeWritten) { 
                stringsToBeWritten.Enqueue(_stringToEnqueue);
            }
        }

        //Scrive a console le stringhe prelevate dalla coda
        private void DoWork()
        {
            while (true)
            {
                if (stringsToBeWritten.Count > 0)
                {
                    Queue<string> temporaryList = GetList();
                    while (temporaryList.Count > 0)
                    {
                        Console.WriteLine(temporaryList.Dequeue());
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        //Svuota la coda delle strighe
        private Queue<string> GetList()
        {
            Queue<string> temporaryList;
            lock (stringsToBeWritten)
            {
                temporaryList = new Queue<string>(stringsToBeWritten);
                stringsToBeWritten.Clear();
            }
            return temporaryList;
        }
    }
}
