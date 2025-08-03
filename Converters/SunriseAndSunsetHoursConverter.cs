using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    public class SunriseAndSunsetHoursConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var dateTime = DateTimeOffset.FromUnixTimeSeconds((int)value).ToLocalTime().DateTime;
                return dateTime.ToString("HH:mm");
            }

            if (value is long)
            {
                var dateTime = DateTimeOffset.FromUnixTimeSeconds((long)value).ToLocalTime().DateTime;
                return dateTime.ToString("HH:mm");
            }

            if (value is double)
            {
                var dateTime = DateTimeOffset.FromUnixTimeSeconds((long)value).ToLocalTime().DateTime;
                return dateTime.ToString("HH:mm");
            }

            return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
