using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public struct Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Column> columns { get; set; }
    }
}
