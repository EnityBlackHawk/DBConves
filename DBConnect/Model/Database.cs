using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Model
{
    public class Database
    {
        public string Name { get;}
        public List<Table> Tables { get;}

        public Database(string name, params Table[] tables)
        {
            Name = name;
            Tables = new(tables);
        }

        public static implicit operator String(Database db) => db.Name;

        public override string ToString() => Name;
    }
}
