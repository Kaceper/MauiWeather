using Newtonsoft.Json.Linq;
using SkiaSharp.Extended.UI.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    public class CodeToLottieConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            float code = System.Convert.ToSingle(value);
            SKFileLottieImageSource lottieImageSource = new SKFileLottieImageSource();

            switch (code)
            {
                case 0:
                    lottieImageSource.File = "sunny.json";
                    return lottieImageSource;
                case 1:
                case 2:
                case 3:
                    lottieImageSource.File = "partly-cloudy.json";
                    return lottieImageSource;
                case 45:
                case 48:
                    lottieImageSource.File = "mist.json";
                    return lottieImageSource;
                case 51:
                case 53:
                case 55:
                    lottieImageSource.File = "rain.json";
                    return lottieImageSource;
                case 56:
                case 57:
                    lottieImageSource.File = "rain.json";
                    return lottieImageSource;
                case 61:
                case 63:
                case 65:
                    lottieImageSource.File = "rain.json";
                    return lottieImageSource;
                case 66:
                case 67:
                    lottieImageSource.File = "rain.json";
                    return lottieImageSource;
                case 71:
                case 73:
                case 75:
                    lottieImageSource.File = "snow.json";
                    return lottieImageSource;
                case 77:
                    lottieImageSource.File = "snow.json";
                    return lottieImageSource;
                case 80:
                case 81:
                case 82:
                    lottieImageSource.File = "rain.json";
                    return lottieImageSource;
                case 85:
                case 86:
                    lottieImageSource.File = "snow.json";
                    return lottieImageSource;
                case 95:
                    lottieImageSource.File = "thunder.json";
                    return lottieImageSource;
                case 96:
                case 99:
                    lottieImageSource.File = "thunder.json";
                    return lottieImageSource;
                default:
                    lottieImageSource.File = "sunny.json";
                    return lottieImageSource;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
