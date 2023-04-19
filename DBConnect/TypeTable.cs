using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBTelegraph
{
    internal static class TypeTable
    {
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
