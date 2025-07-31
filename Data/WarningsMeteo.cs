using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Data
{
    public class WarningsMeteo
    {
        public string id { get; set; }
        public string nazwa_zdarzenia { get; set; }
        public string stopien { get; set; }
        public string prawdopodobienstwo { get; set; }
        public string obowiazuje_do { get; set; }
        public string obowiazuje_od { get; set; }
        public string opublikowano { get; set; }
        public string tresc { get; set; }
        public string komentarz { get; set; }
        public string biuro { get; set; }
        public string[] teryt { get; set; }
        public ObservableCollection<WarningInfo> daily2 { get; set; } = new ObservableCollection<WarningInfo>();
    }

    public class WarningInfo
    {
        public string obowiazuje_do { get; set; }
        public string obowiazuje_od { get; set; }
        public string tresc { get; set; }
    }
}
