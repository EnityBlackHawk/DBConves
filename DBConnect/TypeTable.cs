using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect
{
    internal static class TypeTable
    {
        private static Dictionary<Type, String> Types { get; } = new Dictionary<Type, String>
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
