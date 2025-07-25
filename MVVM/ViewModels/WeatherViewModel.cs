using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Devices.Sensors;

namespace MauiWeather.MVVM.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public WeatherViewModel()
        {
            SearchCommand = new Command(
                async () => await ExecuteSearch(),
                () => !string.IsNullOrWhiteSpace(SearchText)
            );
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged();

                    // ✨ Ważne: ręczne wywołanie przeliczenia CanExecute
                    (SearchCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public ICommand SearchCommand { get; }

        private async Task ExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return;

            var location = await GetCoordinatesAsync(SearchText);

            if (location != null)
            {
                Console.WriteLine($"Znaleziono lokalizację: {location.Latitude}, {location.Longitude}");
            }
            else
            {
                Console.WriteLine("Nie znaleziono lokalizacji");
            }
        }

        private async Task<Location> GetCoordinatesAsync(string address)
        {
            IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);
            return locations?.FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
