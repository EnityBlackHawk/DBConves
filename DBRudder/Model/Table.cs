using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.Model
{
    public class Table : NewObservableObject
    {
        public int Id { get; set; }
        private string _name;
        public string Name 
        { 
            get => _name;
            set
            {
                _name = value.Replace(" ", "_");
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Column> columns { get; set; }

        public static implicit operator DBTelegraph.Model.Table(Table table)
        {
            var list = new List<DBTelegraph.Model.Column>();
            foreach(Column column in table.columns)
            {
                list.Add(column);
            }
            return new DBTelegraph.Model.Table(table.Name, list.ToArray());
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
