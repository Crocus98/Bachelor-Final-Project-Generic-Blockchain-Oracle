using Oracle888730.OracleEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Subscriber GetFromAddress(string address)
        {
            return Subscribers
                .Where(x => x.Address == address)
                .SingleOrDefault();
        }

        public bool AddSubscriber(Subscriber subscriber)
        {
            var c = Subscribers.Where(x => x.Address == subscriber.Address && x.RequestType == subscriber.RequestType).ToList();
            if (c.Count == 0)
            {
                Subscribers.Add(subscriber);
                return this.SaveChanges() > 0;
            }
            return false;
        }
    }
}
