using Microsoft.Extensions.Logging;
using ListaZakupow; 
using CommunityToolkit.Maui; 

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //  Rejestracja serwisów 
        builder.Services.AddSingleton<DatabaseService>(); // baza danych
        builder.Services.AddSingleton<MainPage>(); // Główna strona

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build(); 
    }
}