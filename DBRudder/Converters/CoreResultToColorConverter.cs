using Core.Model;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace DBRudder.Converters
{
    class CoreResultToColorConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return new SolidColorBrush(ColorHelper.FromArgb(244, 68, 68, 68));
            Result r = (Result)value;
            if (r == null)
                return new SolidColorBrush(ColorHelper.FromArgb(244, 68, 68, 68));
            if(r.StatusResult == "Success")
                return Application.Current.Resources["SystemFillColorSuccessBackgroundBrush"];
            if (r.StatusResult == "Error")
                return Application.Current.Resources["SystemFillColorCriticalBackgroundBrush"];
            return new SolidColorBrush(ColorHelper.FromArgb(244, 68, 68, 68));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
