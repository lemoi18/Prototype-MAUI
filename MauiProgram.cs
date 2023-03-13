using CommunityToolkit.Maui;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;


namespace MauiApp8;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<ViewModel.HomePageModel>();
        builder.Services.AddSingleton<ViewModel.SettingsPageModel>();
        builder.Services.AddSingleton<ViewModel.LoginPageModel>();
        

        builder.Services.AddTransient<Views.HomePage>();
        builder.Services.AddSingleton<Views.LoginPage>();
        builder.Services.AddSingleton<Views.SettingsPage>();

        builder.Services.AddSingleton<Services.IAuthenticationService>((e)=> new Services.RefactoredGoogleAuth());


        var app = builder.Build();
        return app;
    }
}
