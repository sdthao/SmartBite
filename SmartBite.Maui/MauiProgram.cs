using MauiIcons.Core;
using MauiIcons.Material;
using SmartBite.Extensions;
using SmartBite.Maui.Views;
using Syncfusion.Licensing;
using CommunityToolkit.Maui;
using SmartBite.Maui.Models;
using NLog.Extensions.Logging;
using SmartBite.Maui.ViewModels;
using MauiIcons.Material.Outlined;
using SmartBite.SQLite.Extensions;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;
using Microsoft.Extensions.Configuration;

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
                .UseMauiCommunityToolkit()
                .UseMaterialMauiIcons()
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

            builder.Logging.ClearProviders();
            builder.Logging.AddNLog("nlog.config");

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "smartbiteusers.db");

            services.AddCore();
            services.AddSQLite(dbPath);

            services.AddSingleton<UserContext>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterUserViewModel>();
            services.AddTransient<EditFoodItemsViewModel>();
            //services.AddTransient<OptionsViewModel>();
            //services.AddTransient<JournalViewModel>();
            //services.AddTransient<CameraViewModel>();
            //services.AddTransient<AIToolViewModel>();


            services.AddTransient<MainPage>();
            services.AddTransient<LoginView>();
            services.AddTransient<RegisterUserView>();
            services.AddTransient<EditFoodItemsView>();
            services.AddTransient<OptionsView>();
            services.AddTransient<UserJournalView>();
            services.AddTransient<CameraView>();
            services.AddTransient<AIToolView>();
        }
    }
}
