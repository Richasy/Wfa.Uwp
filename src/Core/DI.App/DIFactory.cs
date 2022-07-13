// Copyright (c) Richasy. All rights reserved.

using System;
using System.IO;
using System.Threading.Tasks;
using Splat;
using Splat.NLog;
using Wfa.Adapter;
using Wfa.Adapter.Interfaces;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Provider;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel;
using Wfa.ViewModel.LibraryItems;
using Wfa.ViewModel.StateItems;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Wfa.DI.App
{
    /// <summary>
    /// 依赖注入工厂.
    /// </summary>
    public static class DIFactory
    {
        /// <summary>
        /// 注册基本的服务.
        /// </summary>
        public static void RegisterBasicServices()
        {
            // 初始化日志模块.
            var rootFolder = ApplicationData.Current.LocalFolder;
            var logFolderName = AppConstants.LoggerFolder;
            var fullPath = $"{rootFolder.Path}\\{logFolderName}\\";
            NLog.GlobalDiagnosticsContext.Set("LogPath", fullPath);
            Locator.CurrentMutable.UseNLogWithWrappingFullLogger();

            SplatRegistrations.RegisterLazySingleton<IFileToolkit, FileToolkit>();
            SplatRegistrations.RegisterLazySingleton<IResourceToolkit, ResourceToolkit>();
            SplatRegistrations.RegisterLazySingleton<ISettingsToolkit, SettingsToolkit>();
            SplatRegistrations.RegisterLazySingleton<IAppToolkit, AppToolkit>();

            SplatRegistrations.SetupIOC();
        }

        /// <summary>
        /// 注册需要的服务.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public static async Task RegisterRequiredServicesAsync()
        {
            // 初始化数据库.
            await InitializeDatabaseAsync();

            SplatRegistrations.RegisterLazySingleton<ICommunityAdapter, CommunityAdapter>();

            SplatRegistrations.RegisterLazySingleton<IHttpProvider, HttpProvider>();
            SplatRegistrations.RegisterLazySingleton<ICommunityProvider, CommunityProvider>();
            SplatRegistrations.RegisterLazySingleton<IMarketProvider, MarketProvider>();
            SplatRegistrations.RegisterLazySingleton<IStateProvider, StateProvider>();
            SplatRegistrations.RegisterLazySingleton<IWikiProvider, WikiProvider>();

            SplatRegistrations.RegisterLazySingleton<AppViewModel>();
            SplatRegistrations.RegisterLazySingleton<NavigationViewModel>();
            SplatRegistrations.RegisterLazySingleton<WorldStatePageViewModel>();
            SplatRegistrations.RegisterLazySingleton<LibraryPageViewModel>();
            SplatRegistrations.RegisterLazySingleton<LibraryDetailPageViewModel>();
            SplatRegistrations.RegisterLazySingleton<SyndicateMissionPageViewModel>();
            SplatRegistrations.RegisterLazySingleton<InvasionPageViewModel>();
            SplatRegistrations.RegisterLazySingleton<FissurePageViewModel>();

            SplatRegistrations.Register<WorldCycleItemViewModel>();
            SplatRegistrations.Register<LibrarySectionViewModel>();
            SplatRegistrations.Register<WarframeItemViewModel>();
            SplatRegistrations.Register<ArchwingItemViewModel>();
            SplatRegistrations.Register<ArchGunItemViewModel>();
            SplatRegistrations.Register<PrimaryItemViewModel>();
            SplatRegistrations.Register<SecondaryItemViewModel>();

            SplatRegistrations.RegisterConstant(Window.Current.Dispatcher);

            SplatRegistrations.SetupIOC();
        }

        private static async Task InitializeDatabaseAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var path = Path.Combine(localFolder.Path, "lib.db");
            var settingsToolkit = Locator.Current.GetService<ISettingsToolkit>();
            var dbVersion = settingsToolkit.ReadLocalSetting(SettingNames.DbVersion, string.Empty);
            if (!File.Exists(path) || dbVersion != AppConstants.LibraryDbVersion)
            {
                var dbFile = await localFolder.CreateFileAsync("lib.db", CreationCollisionOption.ReplaceExisting);
                var sourceDb = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/lib.db"));
                await sourceDb.CopyAndReplaceAsync(dbFile).AsTask();
            }

            var libContext = new LibraryDbContext(path);
            settingsToolkit.WriteLocalSetting(SettingNames.DbVersion, AppConstants.LibraryDbVersion);
            SplatRegistrations.RegisterConstant(libContext);
        }
    }
}
