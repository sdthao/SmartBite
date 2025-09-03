using MauiIcons.Core;
using MauiIcons.Material.Outlined;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using SmartBite.Maui.ViewModels;
using SmartBite.Maui.Views;
using SmartBite.Services;
using SmartBite.SQLite.Extensions;
using Syncfusion.Licensing;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

namespace SmartBite.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            if (environment == "Development")
            {
                builder.Configuration.AddUserSecrets<App>();

                var syncfusionLicenseKey = builder.Configuration["SyncfusionLicenseKey"];

                if (!string.IsNullOrEmpty(syncfusionLicenseKey))
                {
                    SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenseKey);
                }
            }

            builder.UseMauiApp<App>()
                .UseMauiIconsCore(x => { x.SetDefaultIconAutoScaling(true); })
                .UseMaterialOutlinedMauiIcons()
                .ConfigureSyncfusionToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MauiMaterialAssets.ttf", "MaterialAssets");
                });

            RegisterServices(builder.Services);

            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
            builder.Logging.ClearProviders();
            builder.Logging.AddNLog();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "smartbiteusers.db");

            services.AddSQLite(dbPath);

            services.AddSingleton<UserContext>();

            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterUserViewModel>();


            services.AddTransient<MainPage>();
            services.AddTransient<LoginView>();
            services.AddTransient<RegisterUserView>();
            
        }
    }
}
