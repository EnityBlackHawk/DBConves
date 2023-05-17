using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.CustomElements
{
    public interface IExportableValue
    {
        public string Id { get; set; }
        public object Value { get; set; }
        public Type ExportType { get;}
        public object ExportValue();
    }
}
