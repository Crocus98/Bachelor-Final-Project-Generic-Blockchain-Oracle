using Oracle888730.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Oracle888730.Utility
{
    class ModulesHelper
    {
        public static Type GetType(string _serviceName, string _nameSpace)
        {
            var r = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsClass &&
                    x.ReflectedType == null &&
                    x.Namespace.EndsWith(_nameSpace) &&
                    x.Name.Contains(_serviceName.ToUpper())
                ).FirstOrDefault();
            return r;
        }

        public static List<Type> GetTypes(string _nameSpace)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsClass &&
                    x.ReflectedType == null &&
                    x.Namespace.EndsWith(_nameSpace)
                ).ToList();
        }


        public static T GetInstance<T>(Type _type, object[] _parameters = null)
        {
            return Activator
                .CreateInstance(_type, _parameters) as dynamic;
        }
    }
}
