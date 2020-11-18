using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Oracle888730.OracleEF
{
    partial class OracleContext
    {
        public List<Subscriber> GetAll()
        {
            return Subscribers.ToList();
        }

        public int GetCount()
        {
            int i = 0;
            using (var db = new OracleContext())
            {
                ServiceTypes.Count();
            }
            return i;
        }
        public Subscriber GetDatas()
        {
            Subscriber a = new Subscriber();
            using (var db = new OracleContext())
            {
                a = db.Subscribers.First();
            }
            Console.WriteLine("address "+a.Address + " servicetype " + a.SubscriberId + " foreignkey " + a.ServiceTypeForeignKey + " Coinbase" + a.ServiceType.Service.ServiceName);
            return a;
        }

        public void PutDatas()
        {
            Subscriber a = new Subscriber();
            a.Address = "ASDASD";
            a.ServiceType.ServiceTypeString = "ETH-USD";
            a.ServiceType.ServiceTypeId = 1;
            a.ServiceType.Service.ServiceId = 1;
            a.ServiceType.Service.ServiceName = "COINBASE";
            a.ServiceType.Service.ServiceId = 1;
            using (var db = new OracleContext())
            {
                db.Subscribers.Add(a);
                db.SaveChanges();
            }
            Console.WriteLine("address " + a.Address + " servicetype " + a.SubscriberId + " foreignkey " + a.ServiceTypeForeignKey + " " + a.ServiceType.Service.ServiceName);
        }

        /*
        public Subscriber GetFromAddress(string _address)
        {
            return Subscribers
                .Where(x => x.Address == _address)
                .SingleOrDefault();
        }

        public bool AddSubscriber(Subscriber _subscriber)
        {
            var c = Subscribers.Where(x => x.Address == _subscriber.Address && x.RequestType == _subscriber.RequestType).ToList();
            if (c.Count == 0)
            {
                Subscribers.Add(_subscriber);
                return this.SaveChanges() > 0;
            }
            return false;
        }

        public List<Subscriber> GetListFromSubscriptionType(int _subscriptionType)
        {
            return Subscribers.Where(x => x.RequestType == _subscriptionType).ToList();
        }
        */
    }
}
