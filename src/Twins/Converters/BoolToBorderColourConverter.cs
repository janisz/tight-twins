using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Twins.Converters
{
    public class BoolToBorderColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool) value;
            return new SolidColorBrush(boolValue ? Colors.Black : Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
