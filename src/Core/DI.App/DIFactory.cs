// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using Splat.NLog;
using Wfa.Models.Data.Constants;
using Wfa.Toolkit;
using Wfa.Toolkit.Interfaces;
using Windows.Storage;

namespace Wfa.DI.App
{
    /// <summary>
    /// 依赖注入工厂.
    /// </summary>
    public static class DIFactory
    {
        public static void RegisterRequiredServices()
        {
            var rootFolder = ApplicationData.Current.LocalFolder;
            var logFolderName = AppConstants.LoggerFolder;
            var fullPath = $"{rootFolder.Path}\\{logFolderName}\\";
            NLog.GlobalDiagnosticsContext.Set("LogPath", fullPath);
            Locator.CurrentMutable.UseNLogWithWrappingFullLogger();

            SplatRegistrations.RegisterLazySingleton<IResourceToolkit, ResourceToolkit>();
            SplatRegistrations.RegisterLazySingleton<ISettingsToolkit, SettingsToolkit>();
            SplatRegistrations.RegisterLazySingleton<IAppToolkit, AppToolkit>();
        }
    }
}
