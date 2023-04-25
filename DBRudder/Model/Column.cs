using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public struct Column
    {
        public string Name { get; set; }
        public string Type { get; set; }


        public static List<string> GetSupportedTypes { get; } = DBTelegraph.TypeTable.SupportedTypes;
    }
}
