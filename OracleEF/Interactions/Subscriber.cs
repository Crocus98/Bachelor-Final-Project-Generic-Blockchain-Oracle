﻿using Microsoft.EntityFrameworkCore;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.OracleEF.Models;
using Oracle888730.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Oracle888730.OracleEF
{
    partial class OracleContext
    {
        public bool AddSubscriber(Subscriber _subscriber)
        {
            Subscribers.Add(_subscriber);
            return this.SaveChanges() > 0;
        }

        public List<Subscriber> GetSubscribers(string _service = null, int? _serviceType = null)
        {
            List<Subscriber> entities;
            using (var _context = new OracleContext())
            {
               entities = (from x in _context.Subscribers.Include("ServiceType.Service")
                          where (_service == null || x.ServiceType.Service.ServiceName == _service)
                          && (_serviceType == null || x.ServiceType.ServiceTypeId == _serviceType)
                          select x).ToList();
            }
            return entities;
        }
    }
}
