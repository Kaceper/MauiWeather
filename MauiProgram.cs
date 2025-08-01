#if ANDROID
using Android.Views;
using AndroidX.Core.View;
using Google.Android.Material.AppBar;


#endif

using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Text;

namespace MauiWeather
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("UnhandledException: " + e.ExceptionObject);
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("UnobservedTaskException: " + e.Exception.Message);
                e.SetObserved();
            };

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Lato-Light.ttf", "LatoLight");
                    fonts.AddFont("Lato-Regular.ttf", "LatoRegular");
                });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return builder.Build();
        }
    }
}
