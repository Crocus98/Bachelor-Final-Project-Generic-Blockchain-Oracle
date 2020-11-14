using Nethereum.RPC.Shh.DTOs;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
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
            Task writeMessages = new Task(DoWork);
            writeMessages.ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
            writeMessages.Start();
        }

        public static void Enqueue(string _stringToEnqueue)
        {
            stringsToBeWritten.Enqueue(_stringToEnqueue);
        }

        private void ExceptionHandler(Task _writeMessages)
        {
            var exception = _writeMessages.Exception;
            Console.WriteLine(message + "[ERROR] " + exception.Message);
        }

        private void DoWork()
        {
            while (true)
            {
                if (stringsToBeWritten.Count > 0)
                {
                    Console.WriteLine(stringsToBeWritten.Dequeue());
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
}
