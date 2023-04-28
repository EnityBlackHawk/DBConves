using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Converters
{
    public class StringToInfoBarSeverityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string s = value as string;
            if(s == InfoBarSeverity.Error.ToString())
                return InfoBarSeverity.Error;
            else if(s == InfoBarSeverity.Success.ToString())
                return InfoBarSeverity.Success;
            else if(s == InfoBarSeverity.Warning.ToString())
                return InfoBarSeverity.Warning;
            return InfoBarSeverity.Informational;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((InfoBarSeverity)value).ToString();
        }
    }
}
