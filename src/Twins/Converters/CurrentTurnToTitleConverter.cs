using System;
using System.Globalization;
using System.Windows.Data;

namespace Twins.Converters
{
    public class CurrentTurnToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var turnValue = (Turn) value;
            return turnValue == Turn.First ? "First player's turn" : "Second player's turn";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
