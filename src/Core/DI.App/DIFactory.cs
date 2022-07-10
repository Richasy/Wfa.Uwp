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
using Wfa.Provider;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel;
using Wfa.ViewModel.Items;
using Windows.Storage;

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

            SplatRegistrations.Register<WorldCycleItemViewModel>();

            SplatRegistrations.SetupIOC();
        }

        private static async Task InitializeDatabaseAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var path = Path.Combine(localFolder.Path, "lib.db");
            if (!File.Exists(path))
            {
                var dbFile = await localFolder.CreateFileAsync("lib.db", CreationCollisionOption.ReplaceExisting);
                var sourceDb = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/lib.db"));
                await sourceDb.CopyAndReplaceAsync(dbFile).AsTask();
            }

            var libContext = new LibraryDbContext(path);
            SplatRegistrations.RegisterConstant(libContext);
        }
    }
}
