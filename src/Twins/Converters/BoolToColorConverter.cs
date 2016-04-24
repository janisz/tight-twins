using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Twins.Helpers;

namespace Twins.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (int?) value;
            if (boolValue == null)
                return new SolidColorBrush(Colors.White);
            var convertFromString = ColorConverter.ConvertFromString(ColorsProvider.Colors[(int)boolValue]);
            if (convertFromString != null)
                return new SolidColorBrush((System.Windows.Media.Color)convertFromString);
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
