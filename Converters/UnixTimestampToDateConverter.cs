using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    public class UnixTimestampToDateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            DateTime dateTime;

            if (value is int intVal)
            {
                dateTime = DateTimeOffset.FromUnixTimeSeconds(intVal).DateTime;
            }
            else if (value is long longVal)
            {
                dateTime = DateTimeOffset.FromUnixTimeSeconds(longVal).DateTime;
            }
            else if (value is double doubleVal)
            {
                dateTime = DateTimeOffset.FromUnixTimeSeconds((long)doubleVal).DateTime;
            }
            else
            {
                return "";
            }

            // Pobieram nazwę dnia tygodnia po polsku
            string day = dateTime.ToString("dddd", new CultureInfo("pl-PL"));

            // Zwracam z wielką literą na początku
            return char.ToUpper(day[0]) + day.Substring(1);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
