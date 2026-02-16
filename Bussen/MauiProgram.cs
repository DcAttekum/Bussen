using Bussen.Services;
using Bussen.ViewModels;
using Bussen.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace Bussen
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<GameService>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SetupViewModel>();
            builder.Services.AddTransient<DealViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<SetupViewModel>();
            builder.Services.AddTransient<DealPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
