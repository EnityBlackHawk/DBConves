using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.Model
{
    public class Column : NewObservableObject
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value.Replace(" ", "_"); OnPropertyChanged(); }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }

        private bool _isPrimaryKey;

        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; OnPropertyChanged(); }
        }


        public static List<string> GetSupportedTypes { get; } = DBTelegraph.TypeTable.SupportedTypes;
        
        public static implicit operator DBTelegraph.Model.Column(Column column)
        {
            var constraints = new List<DBTelegraph.Model.Constraints>();
            if (column.IsPrimaryKey) constraints.Add(DBTelegraph.Model.Constraints.PRIMARY_KEY);
            return new DBTelegraph.Model.Column(column.Name, DBTelegraph.TypeTable.GetTypeOfString(column.Type), constraints.ToArray());
        }
    }
}
