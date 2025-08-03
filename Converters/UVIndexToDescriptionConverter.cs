using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    public class UVIndexToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Brak danych";

            if (!double.TryParse(value.ToString(), out double uvIndex))
                return "Nieprawidłowa wartość";

            if (uvIndex < 0)
                return "Nieprawidłowa wartość";

            if (uvIndex <= 2)
                return "Niski";
            else if (uvIndex <= 5)
                return "Umiarkowany";
            else if (uvIndex <= 7)
                return "Wysoki";
            else if (uvIndex <= 10)
                return "Bardzo wysoki";
            else
                return "Ekstremalny";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
