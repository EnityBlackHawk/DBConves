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

    }
}
