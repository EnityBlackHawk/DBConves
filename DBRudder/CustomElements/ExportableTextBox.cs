using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.CustomElements
{
    public class ExportableTextBox : TextBox, IExportableValue
    {
        public string Id { get; set; }
        public object Value { get => base.Text; set => Text = (string)value; }
        public Type ExportType { get => typeof(string);}

        public object ExportValue()
        {
            throw new NotImplementedException();
        }
    }
}
