// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Windows.ApplicationModel;
using Windows.Globalization;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 设置页面视图模型.
    /// </summary>
    public sealed partial class SettingsPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPageViewModel"/> class.
        /// </summary>
        public SettingsPageViewModel(
            AppViewModel appViewModel,
            LibraryDbContext dbContext,
            ISettingsToolkit settingsToolkit,
            IResourceToolkit resourceToolkit,
            IAppToolkit appToolkit)
        {
            _appViewModel = appViewModel;
            _dbContext = dbContext;
            _settingsToolkit = settingsToolkit;
            _resourceToolkit = resourceToolkit;
            _appToolkit = appToolkit;

            WikiTypes = new ObservableCollection<WikiType>();

            ActiveCommand = ReactiveCommand.Create(InitializeSettings);
            DeactiveCommand = ReactiveCommand.Create(() => { });

            _appViewModel.PropertyChanged += OnAppViewModelPropertyChangedAsync;
        }

        /// <summary>
        /// 初始化设置.
        /// </summary>
        public void InitializeSettings()
        {
            PropertyChanged -= OnPropertyChangedAsync;
            AppTheme = ReadSetting(SettingNames.AppTheme, AppConstants.ThemeDefault);
            _initializeTheme = AppTheme;
            IsPrelaunch = ReadSetting(SettingNames.IsPrelaunch, true);
            IsCommunityDatabaseAutoUpdate = ReadSetting(SettingNames.IsCommunityDatabaseAutoUpdate, false);
            WikiInit();
            StartupInitAsync();
            DatabaseUpdateTimeInitAsync();

            Version = _appToolkit.GetPackageVersion();
            PropertyChanged += OnPropertyChangedAsync;
        }

        private async void OnPropertyChangedAsync(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AppTheme):
                    WriteSetting(SettingNames.AppTheme, AppTheme);
                    IsShowThemeRestartTip = AppTheme != _initializeTheme;
                    break;
                case nameof(IsPrelaunch):
                    WriteSetting(SettingNames.IsPrelaunch, IsPrelaunch);
                    SetPrelaunch();
                    break;
                case nameof(IsStartup):
                    await TrySetStartupAsync();
                    break;
                case nameof(PreferWiki):
                    WriteSetting(SettingNames.PreferWiki, PreferWiki);
                    break;
                case nameof(IsCommunityDatabaseAutoUpdate):
                    WriteSetting(SettingNames.IsCommunityDatabaseAutoUpdate, IsCommunityDatabaseAutoUpdate);
                    break;
                default:
                    break;
            }
        }

        private void WikiInit()
        {
            if (WikiTypes.Count == 0)
            {
                WikiTypes.Add(WikiType.Fandom);
                WikiTypes.Add(WikiType.Huiji);
            }

            var defaultWiki = ApplicationLanguages.Languages.First().Contains("zh", StringComparison.OrdinalIgnoreCase)
                ? WikiType.Huiji
                : WikiType.Fandom;
            PreferWiki = ReadSetting(SettingNames.PreferWiki, defaultWiki);
        }

        private async void StartupInitAsync()
        {
            var task = await StartupTask.GetAsync(AppConstants.StartupTaskId);
            IsStartup = task.State.ToString().Contains("enable", StringComparison.OrdinalIgnoreCase);
            StartupWarningText = string.Empty;
        }

        private async void DatabaseUpdateTimeInitAsync()
        {
            var communityTime = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WarframeItemsUpdateTimeKey);
            var marketTime = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WarframeMarketUpdateTimeKey);

            if (communityTime != null)
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(communityTime.Value)).ToLocalTime();
                CommunityDatabaseUpdateTime = string.Format(_resourceToolkit.GetLocaleString(LanguageNames.LastUpdateTimeFormatText), date.ToString("yyyy/MM/dd HH:mm"));
            }
            else
            {
                CommunityDatabaseUpdateTime = _resourceToolkit.GetLocaleString(LanguageNames.NeverUpdate);
            }

            if (marketTime != null)
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(marketTime.Value)).ToLocalTime();
                MarketDatabaseUpdateTime = string.Format(_resourceToolkit.GetLocaleString(LanguageNames.LastUpdateTimeFormatText), date.ToString("yyyy/MM/dd HH:mm"));
            }
            else
            {
                MarketDatabaseUpdateTime = _resourceToolkit.GetLocaleString(LanguageNames.NeverUpdate);
            }
        }

        private void WriteSetting(SettingNames name, object value) => _settingsToolkit.WriteLocalSetting(name, value);

        private T ReadSetting<T>(SettingNames name, T defaultValue) => _settingsToolkit.ReadLocalSetting(name, defaultValue);
    }
}
