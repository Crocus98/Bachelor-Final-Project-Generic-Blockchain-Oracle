using System.Threading;

namespace Oracle888730.Classes
{
    interface IGeneric
    {
        //Metodo che avvia i thread su cui verranno eseguiti i metodi delle classi che ereditano da questa interfaccia
        public Thread Start();

    }
}
