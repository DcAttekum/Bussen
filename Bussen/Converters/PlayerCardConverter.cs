using Bussen.Models;
using System.Globalization;

namespace Bussen.Converters
{
    /// <summary>
    /// Player card converter to assign to the front end.
    /// </summary>
    public class PlayerCardConverter : IMultiValueConverter
    {
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            PlayerCard? returnValue = null;

            if (values.Length == 2 && values[0] != null && values[1] != null)
            {
                returnValue = new PlayerCard(values[0] as Player, values[1] as Card);
            }

            return returnValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
