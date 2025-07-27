using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Converters
{
    public class CodeToWeatherConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            float code = System.Convert.ToSingle(value);

            switch (code)
            {
                case 0:
                    return "Pogodnie";
                case 1:
                case 2:
                case 3:
                    return "Częściowe zachmurzenie";
                case 45:
                case 48:
                    return "Mgła";
                case 51:
                case 53:
                case 55:
                    return "Mżawka";
                case 56:
                case 57:
                    return "Marznąca mżawka";
                case 61:
                case 63:
                case 65:
                    return "Deszcz";
                case 66:
                case 67:
                    return "Marznący deszcz";
                case 71:
                case 73:
                case 75:
                    return "Śnieg";
                case 77:
                    return "Śnieg ziarnisty";
                case 80:
                case 81:
                case 82:
                    return "Przelotny deszcz";
                case 85:
                case 86:
                    return "Przelotny śnieg";
                case 95:
                    return "Burze";
                case 96:
                case 99:
                    return "Burze z gradem";
                default:
                    return "Nieznana pogoda";
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
