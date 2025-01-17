using Microsoft.Extensions.Logging;
using ayushChaudhary.Service;
using MudBlazor.Services;

namespace ayushChaudhary
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
                });

            builder.Services.AddMauiBlazorWebView();

            // Registering User Services
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<CreditService>(); // Register the CreditService

            // Register DebitService

            // Register other services
            builder.Services.AddSingleton<AuthenticationStateService>();

            builder.Services.AddSingleton<DebitService>(); // Register DebitService as a Singleton
            builder.Services.AddSingleton<DebtService>();



            // MudBlazor services
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
