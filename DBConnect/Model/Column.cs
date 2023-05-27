using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTelegraph.Model
{

    public enum Constraints
    {
        PRIMARY_KEY,
        FOREIGN_KEY 
    }

    public class Column
    {

        public string Name { get; set; }

        public List<Constraints> Constraints { get; set; }

        public Type Type { get; set; }

        public Column(string name, Type type, string? refCol = null, string? refTable = null, params Constraints[] constraints)
        {
            Name = name;
            Type = type;
            Constraints = new List<Constraints>(constraints);

            ReferencesTable = refTable;
            ReferecesColumn = refCol;

        }

        public string? ReferecesColumn { get; set; }

        public string? ReferencesTable { get; set; }

        public bool IsForeignKey => Constraints.Contains(Model.Constraints.FOREIGN_KEY);

        public static implicit operator Type(Column column)
        {
            return column.Type;
        }
    }
}
