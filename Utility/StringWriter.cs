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
        private string message;

        public StringWriter()
        {
            stringsToBeWritten = new Queue<string>();
            message = "[StringWriter]";
            this.Start();
            StringWriter.Enqueue(message + " Service started successfully...");
        }

        public void Start()
        {
            Thread writeMessages = new Thread(DoWork);
            writeMessages.Start();
        }

        public static void Enqueue(string _stringToEnqueue)
        {
            lock (stringsToBeWritten) { 
                stringsToBeWritten.Enqueue(_stringToEnqueue);
            }
        }

        private void DoWork()
        {
            while (true)
            {
                if (stringsToBeWritten.Count > 0)
                {
                    Queue<string> temporaryList; 
                    lock (stringsToBeWritten)
                    {
                        temporaryList = new Queue<string>(stringsToBeWritten);
                        stringsToBeWritten.Clear();
                    }
                    while(temporaryList.Count > 0)
                    {
                        Console.WriteLine(temporaryList.Dequeue());
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
