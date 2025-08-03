using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    public class WindDirectionConverter : IValueConverter
    {
        private static readonly string[] Directions = new[]
        {
            "płn",       // 0°
            "płn-wsch",  // 22.5°
            "wsch",      // 45°
            "poł-wsch",  // 67.5°
            "poł",       // 90°
            "poł-zach",  // 112.5°
            "zach",      // 135°
            "płn-zach",  // 157.5°
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float deg)
            {
                // Podział na 8 sektorów co 45°, z przesunięciem o 22.5°
                var index = (int)Math.Round(((deg % 360) / 45), MidpointRounding.AwayFromZero) % 8;
                return $"\nSkąd: {Directions[index]}";
            }

            return "\nSkąd: –";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
