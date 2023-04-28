using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBTelegraph
{
    public static class TypeTable
    {

        public static List<string> SupportedTypes { get; } = new List<string>
        {
            "Int32",
            "Float",
            "String",
            "DateTime"
        };

        private static Dictionary<string, Type> _stringToType = new Dictionary<string, Type>
        {
            { "Int32", typeof(int)},
            {"Float", typeof(float)},
            {"String", typeof(string) },
            {"DateTime", typeof(DateTime)}
        };

        private static Dictionary<Type, string> Types { get; } = new Dictionary<Type, string>
        {
            {typeof(int), "int"},
            {typeof(float), "float" },
            {typeof(string), "text"},
            {typeof(DateTime), "datetime"},
        };

        public static string getSQLType(Type type)
        {
            return Types[type];
        }

        public static Type GetTypeOfString(string typeName) => _stringToType[typeName];
        
    }
}
