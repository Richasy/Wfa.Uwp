// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using Windows.ApplicationModel;
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

            SplatRegistrations.RegisterLazySingleton<IResourceToolkit, ResourceToolkit>();
            SplatRegistrations.RegisterLazySingleton<ISettingsToolkit, SettingsToolkit>();
            SplatRegistrations.RegisterLazySingleton<IAppToolkit, AppToolkit>();
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

            SplatRegistrations.RegisterLazySingleton<ICommunityProvider, CommunityProvider>();
        }

        private static async Task InitializeDatabaseAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            if (File.Exists(Path.Combine(localFolder.Path, "data.db")))
            {
                return;
            }

            var dbFile = await localFolder.CreateFileAsync("data.db", CreationCollisionOption.ReplaceExisting);
            var sourceDb = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/data.db"));
            await sourceDb.CopyAndReplaceAsync(dbFile).AsTask();

            var libContext = new LibraryDbContext(dbFile.Path);
            SplatRegistrations.RegisterConstant(libContext);
        }
    }
}
