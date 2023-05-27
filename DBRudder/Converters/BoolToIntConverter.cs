using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Converters
{
    class BoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? 1 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((int)value == 1);
        }
    }
}
