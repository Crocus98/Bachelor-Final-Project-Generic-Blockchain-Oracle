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
        }

        public void Start()
        {
            Thread writeMessages = new Thread(DoWork);
            writeMessages.Start();
        }

        public static void Enqueue(string _stringToEnqueue)
        {
            //Monitor.TryEnter(stringsToBeWritten);
            lock (stringsToBeWritten) { 
                stringsToBeWritten.Enqueue(_stringToEnqueue); 
            }
            //Monitor.Exit(stringsToBeWritten);

        }

        /*private void ExceptionHandler(Task _writeMessages)
        {
            var exception = _writeMessages.Exception;
            Console.WriteLine(message + "[ERROR] " + exception.Message);
        }*/

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
