// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.Models.Data.Local;
using Wfa.Toolkit.Interfaces;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 应用更新提醒对话框.
    /// </summary>
    public sealed partial class AppUpgradeDialog : ContentDialog
    {
        private readonly AppUpgradeEventArgs _eventArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUpgradeDialog"/> class.
        /// </summary>
        public AppUpgradeDialog() => InitializeComponent();

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeDialog"/> class.
        /// </summary>
        /// <param name="args">升级参数.</param>
        public AppUpgradeDialog(AppUpgradeEventArgs args)
            : this()
        {
            _eventArgs = args;
            Initialize();
        }

        private void Initialize()
        {
            TitleBlock.Text = _eventArgs.ReleaseTitle;
            PreReleaseContainer.Visibility = _eventArgs.IsPreRelease ? Visibility.Visible : Visibility.Collapsed;
            PublishTimeBlock.Text = _eventArgs.PublishTime.ToString("yyyy/MM/dd HH:mm");
            MarkdownBlock.Text = _eventArgs.ReleaseDescription;
        }

        private async void ContentDialog_PrimaryButtonClickAsync(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => await Launcher.LaunchUriAsync(_eventArgs.DownloadUrl);

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var settingToolkit = Locator.Current.GetService<ISettingsToolkit>();
            settingToolkit.WriteLocalSetting(Models.Enums.SettingNames.IgnoreVersion, _eventArgs.Version);
        }
    }
}
