// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.Models.Data.Constants;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;

namespace Wfa.App.Controls.Settings
{
    /// <summary>
    /// 日志设置区块.
    /// </summary>
    public sealed partial class LoggerSettingSection : SettingSectionControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerSettingSection"/> class.
        /// </summary>
        public LoggerSettingSection()
            : base()
            => InitializeComponent();

        private async void OnOpenLoggerFolderButtonClickAsync(object sender, RoutedEventArgs e)
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(AppConstants.LoggerFolder, CreationCollisionOption.OpenIfExists).AsTask();
            await Launcher.LaunchFolderAsync(folder);
        }

        private async void OnCleanLoggerButtonClickAsync(object sender, RoutedEventArgs e)
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(AppConstants.LoggerFolder, CreationCollisionOption.OpenIfExists).AsTask();
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            try
            {
                await folder.DeleteAsync(StorageDeleteOption.PermanentDelete).AsTask();
                await ApplicationData.Current.LocalFolder.CreateFolderAsync(AppConstants.LoggerFolder, CreationCollisionOption.OpenIfExists).AsTask();
            }
            catch (Exception)
            {
            }
            finally
            {
                CoreViewModel.ShowTip(resourceToolkit.GetLocaleString(LanguageNames.LogEmptied), InfoType.Success);
            }
        }
    }
}
