using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Oracle888730.Utility
{
    class ModulesHelper
    {
        //Ottiene il tipo dalla stringa
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

        //Ottiene la lista dei tipi nel namespace
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

        //Crea e restituisce l'istanza del tipo passato come parametro
        public static T GetInstance<T>(Type _type, object[] _parameters = null)
        {
            return Activator
                .CreateInstance(_type, _parameters) as dynamic;
        }
    }
}
