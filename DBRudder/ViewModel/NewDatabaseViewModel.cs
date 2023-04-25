using DBRudder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.ViewModel
{
    public class NewDatabaseViewModel : NewObservableObject
    {
        public ObservableCollection<Model.Table> Tables { get; }

        public NewDatabaseViewModel()
        {
            Tables = new ObservableCollection<Model.Table>
            {
                new Model.Table {Id = 0, Name = "Table 1", columns = new ObservableCollection<Column>{ new Column{Name = " Column 1", Type = "float"}} },
                new Model.Table {Id = 1, Name = "Table 2", columns = new ObservableCollection<Column>{ new Column{Name = " Column 1", Type = "float"}} }
            };
        }
    }
}
