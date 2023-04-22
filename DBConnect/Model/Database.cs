using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DBTelegraph.Model
{
    public class Database
    {
        public string Name { get; }
        public List<Table> Tables { get; }

        public Database(string name, params Table[] tables)
        {
            Name = name;
            Tables = new(tables);
        }

        public void AddTable(Table table) => Tables.Add(table);

        public static implicit operator string(Database db) => db.Name;

        public override string ToString() => Name;
    }
}
