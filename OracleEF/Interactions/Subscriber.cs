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
            return Subscribers.Count();
        }

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
    }
}
