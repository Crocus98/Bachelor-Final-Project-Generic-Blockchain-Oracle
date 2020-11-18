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
        /**
         * string moduleNamespace = "StategyPattern.Modules";
            var moduleList = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && x.Namespace == moduleNamespace).ToList();
            moduleList.ForEach(x => Console.WriteLine(x.Name));
        */
        private static string Namespace = "Classes.Handlers";
        private static Type GetHandler(string serviceName)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    x.IsClass &&
                    x.Namespace == Namespace &&
                    x.Name.Contains(serviceName.ToUpper())
                ).FirstOrDefault();
        }

        public static List<Type> GetHandlers()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    x.IsClass &&
                    x.Namespace == Namespace
                ).ToList();
        }

        public static GenericRequestHandler GetInstance(string serviceName)
        {
            var handler = GetHandler(serviceName);
            if (handler == null) return null;
            return Activator.CreateInstance(handler) as GenericRequestHandler;
        }
    }
}
