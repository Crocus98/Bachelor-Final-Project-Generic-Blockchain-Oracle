using Oracle888730.OracleEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Oracle888730.OracleEF
{
    public partial class OracleContext
    {
        //Ottiene l'oggetto Service type
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
