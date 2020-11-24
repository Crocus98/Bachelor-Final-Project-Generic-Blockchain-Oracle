using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Text;
using Oracle888730.OracleEF.Models;
using Microsoft.EntityFrameworkCore;
using Oracle888730.Contracts.Oracle888730.ContractDefinition;
using Oracle888730.Utility;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Oracle888730.OracleEF
{
    partial class OracleContext
    {
        //Ottiene il Service type e il Service
        public static ServiceType GetRequestedType(string _serviceName,int _serviceTypeId)
        {
            ServiceType entity;
            using (var _context = new OracleContext())
            {
                entity = (from x in _context.ServiceTypes.Include("Service")
                          where x.ServiceTypeId == _serviceTypeId 
                          && x.Service.ServiceName == _serviceName
                          select x).FirstOrDefault();
            }
            return entity;
        }
    }
}
