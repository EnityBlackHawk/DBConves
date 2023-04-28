using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public class Database
    {
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        
        public Database(string name, List<Table> tables)
        {
            Name = name;
            Tables = tables;
        }

        public static implicit operator DBTelegraph.Model.Database(Database db)
        {
            List<DBTelegraph.Model.Table> list = new List<DBTelegraph.Model.Table>();
            db.Tables.ForEach(t => list.Add(t));
            DBTelegraph.Model.Database result = new DBTelegraph.Model.Database(db.Name, list.ToArray());
            return result;
        }

    }
}
