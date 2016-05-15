using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Twins.Converters
{
    public class TwinNumberToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var twinValue = (int?) value;
            SolidColorBrush color;

            switch (twinValue)
            {
                case 0:
                    color = Brushes.LightCyan;
                    break;
                case 1:
                    color = Brushes.Crimson;
                    break;
                default:
                    color = Brushes.Transparent;
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
