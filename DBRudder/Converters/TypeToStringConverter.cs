using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Converters
{
    public class TypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((Type)value).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Type.GetType(value as String);
        }
    }
}
