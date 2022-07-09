// Copyright (c) Richasy. All rights reserved.

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Windows.Globalization;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 应用视图模型.
    /// </summary>
    public sealed partial class AppViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel"/> class.
        /// </summary>
        public AppViewModel(
            ISettingsToolkit settingsToolkit,
            IResourceToolkit resourceToolkit,
            ICommunityProvider communityProvider,
            IMarketProvider marketProvider,
            IWikiProvider wikiProvider,
            IStateProvider stateProvider,
            LibraryDbContext dbContext)
        {
            _settingsToolkit = settingsToolkit;
            _resourceToolkit = resourceToolkit;

            _communityProvider = communityProvider;
            _marketProvider = marketProvider;
            _wikiProvider = wikiProvider;
            _stateProvider = stateProvider;
            _dbContext = dbContext;

            _isWide = null;
            IsNavigatePaneOpen = true;
            IsShowTitleBar = true;

            _stateTimer = new Windows.UI.Xaml.DispatcherTimer();
            _stateTimer.Interval = TimeSpan.FromMinutes(1.5);
            _stateTimer.Tick += OnStateTimerTick;

            _stateProvider.StateChanged += OnWorldStateChanged;

            CheckLibraryDatabaseCommand = ReactiveCommand.CreateFromTask<bool>(CheckLibraryDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            CheckWarframeMarketDatabaseCommand = ReactiveCommand.CreateFromTask(CheckWaframeMarketDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            CheckTranslateDatabaseCommand = ReactiveCommand.CreateFromTask(CheckTranslateDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            CheckPatchDatabaseCommand = ReactiveCommand.CreateFromTask(CheckPatchDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);

            UpdateLibraryDatabaseCommand = ReactiveCommand.CreateFromTask<string>(UpdateLibraryDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            UpdateWarframeMarketDatabaseCommand = ReactiveCommand.CreateFromTask(UpdateWarframeMarketDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            UpdateTranslateDatabaseCommand = ReactiveCommand.CreateFromTask(UpdateTranslateDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            UpdatePatchDatabaseCommand = ReactiveCommand.CreateFromTask(UpdatePatchDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);

            RequestWorldStateCommand = ReactiveCommand.CreateFromTask(RequestWorldStateAsync, outputScheduler: RxApp.MainThreadScheduler);
            BeginLoopWorldStateCommand = ReactiveCommand.CreateFromTask(BeginLoopWorldStateAsync, outputScheduler: RxApp.MainThreadScheduler);

            _isRequestingState = RequestWorldStateCommand.IsExecuting.ToProperty(this, x => x.IsRequestingState, scheduler: RxApp.MainThreadScheduler);

            CheckLibraryDatabaseCommand.ThrownExceptions
                .Merge(CheckWarframeMarketDatabaseCommand.ThrownExceptions)
                .Merge(CheckTranslateDatabaseCommand.ThrownExceptions)
                .Merge(CheckPatchDatabaseCommand.ThrownExceptions)
                .Merge(UpdateWarframeMarketDatabaseCommand.ThrownExceptions)
                .Merge(UpdateLibraryDatabaseCommand.ThrownExceptions)
                .Merge(UpdateTranslateDatabaseCommand.ThrownExceptions)
                .Merge(UpdatePatchDatabaseCommand.ThrownExceptions)
                .Merge(RequestWorldStateCommand.ThrownExceptions)
                .Merge(BeginLoopWorldStateCommand.ThrownExceptions)
                .Subscribe(LogException);
        }

        /// <summary>
        /// 初始化语言选项.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public async Task InitializeLanguageAsync()
        {
            var appLanguage = ApplicationLanguages.Languages.First();
            var supportLan = AppConstants.LanguageEn;
            if (appLanguage.Contains("zh", StringComparison.OrdinalIgnoreCase))
            {
                supportLan = appLanguage.Contains("cn", StringComparison.OrdinalIgnoreCase) || appLanguage.Contains("hans", StringComparison.OrdinalIgnoreCase)
                    ? AppConstants.LanguageChs
                    : AppConstants.LanguageCht;
            }

            var localLan = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey);
            var lanChanged = localLan == null || localLan.Value != supportLan;
            _settingsToolkit.WriteLocalSetting(SettingNames.LanguageNeedInitialized, lanChanged);
            if (localLan == null)
            {
                await _dbContext.Metas.AddAsync(new Models.Data.Center.Meta()
                {
                    Name = AppConstants.LanguageKey,
                    Value = supportLan,
                });
            }
            else
            {
                localLan.Value = supportLan;
                _dbContext.Metas.Update(localLan);
            }

            if (lanChanged)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 初始化边距设置.
        /// </summary>
        public void InitializePadding()
        {
            var width = Window.Current.Bounds.Width;
            var isWide = _isWide.HasValue && _isWide.Value;
            if (width >= MediumWindowThresholdWidth)
            {
                if (!isWide)
                {
                    _isWide = true;
                    PageHorizontalPadding = _resourceToolkit.GetResource<Thickness>("DefaultPageHorizontalPadding");
                    PageTopPadding = _resourceToolkit.GetResource<Thickness>("DefaultPageTopPadding");
                }
            }
            else
            {
                _isWide = false;
                PageHorizontalPadding = _resourceToolkit.GetResource<Thickness>("NarrowPageHorizontalPadding");
                PageTopPadding = _resourceToolkit.GetResource<Thickness>("NarrowPageTopPadding");
            }
        }

        private async Task CheckLibraryDatabaseAsync(bool ignoreDate = false)
        {
            var communityUpdateCheckResult = await _communityProvider.CheckUpdateAsync(ignoreDate);
            if (communityUpdateCheckResult.NeedUpdate)
            {
                WriteMessage($"社区资料库需要更新，远端版本：{communityUpdateCheckResult.RemoteVersion}");
                await UpdateLibraryDatabaseAsync(communityUpdateCheckResult.RemoteVersion);
            }
            else
            {
                WriteMessage("社区数据已经更新至最新");
            }
        }

        /// <summary>
        /// 如果 WM 数据库内容为空，则进行更新.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        private async Task CheckWaframeMarketDatabaseAsync()
        {
            var meta = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WarframeMarketUpdateTimeKey);
            if (!string.IsNullOrEmpty(meta?.Value))
            {
                // 不需要初始化.
                WriteMessage("WM 数据已初始化完成");
                return;
            }

            await UpdateWarframeMarketDatabaseAsync();
        }

        private async Task CheckTranslateDatabaseAsync()
        {
            var meta = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WikiDictUpdateTimeKey);
            if (!string.IsNullOrEmpty(meta?.Value))
            {
                // 不需要初始化.
                WriteMessage("维基翻译数据已初始化完成");
                return;
            }

            await UpdateTranslateDatabaseAsync();
        }

        private async Task CheckPatchDatabaseAsync()
        {
            var meta = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WikiPatchUpdateTimeKey);
            if (!string.IsNullOrEmpty(meta?.Value))
            {
                // 不需要初始化.
                WriteMessage("维基简繁互译数据已初始化完成");
                return;
            }

            await UpdatePatchDatabaseAsync();
        }

        private async Task UpdateLibraryDatabaseAsync(string remoteVersion = default)
        {
            WriteMessage($"开始缓存所需文件...");

            // TODO: 显示更新UI.
            if (string.IsNullOrEmpty(remoteVersion))
            {
                var communityUpdateCheckResult = await _communityProvider.CheckUpdateAsync(true);
                remoteVersion = communityUpdateCheckResult.RemoteVersion;
            }

            var cacheResult = await _communityProvider.CacheLibraryFilesAsync();
            foreach (var cacheItem in cacheResult)
            {
                WriteMessage($"{cacheItem.Key}: {cacheItem.Value}");
            }

            WriteMessage("缓存结束，开始更新词典数据");
            await _communityProvider.UpdateDictAsync();

            WriteMessage("词典数据更新完成，开始更新其它内容");
            var updateArray = new[]
            {
                CommunityDataType.Warframe,
                CommunityDataType.ArchGun,
                CommunityDataType.ArchMelee,
                CommunityDataType.Archwing,
                CommunityDataType.Melee,
                CommunityDataType.Primary,
                CommunityDataType.Secondary,
                CommunityDataType.Mod,
            };

            foreach (var updateKey in updateArray)
            {
                WriteMessage($"更新内容 {updateKey} ...");
                await _communityProvider.UpdateDataAsync(updateKey);
                WriteMessage($"更新 {updateKey} 完成");
            }

            WriteMessage("全部内容更新完成，正在清理缓存");
            await _communityProvider.CommitLibraryVersionAsync(remoteVersion);
            WriteMessage("缓存已清理，记录此次更新 Id");
        }

        private async Task UpdateWarframeMarketDatabaseAsync()
        {
            WriteMessage($"开始更新 Warframe Market 资料...");

            var updateArray = new[]
            {
                MarketDataType.Items,
                MarketDataType.LichWeapons,
                MarketDataType.LichEpemeras,
                MarketDataType.LichQuirks,
                MarketDataType.RivenWeapons,
                MarketDataType.RivenAttributes,
            };

            foreach (var updateKey in updateArray)
            {
                WriteMessage($"更新内容 {updateKey} ...");
                await _marketProvider.UpdateDataAsync(updateKey);
                WriteMessage($"更新 {updateKey} 完成");
            }

            WriteMessage("市场数据更新完成，正在提交更新时间");
            await _marketProvider.CommitUpdateTimeAsync();
            WriteMessage("已记录此次更新时间");
        }

        private async Task UpdateTranslateDatabaseAsync()
        {
            WriteMessage("开始更新维基翻译数据");
            await _wikiProvider.UpdateTranslatesAsync();
            WriteMessage("维基翻译数据更新完成");
        }

        private async Task UpdatePatchDatabaseAsync()
        {
            WriteMessage("开始更新维基简繁互译数据");
            await _wikiProvider.UpdatePatchesAsync();
            WriteMessage("简繁互译数据更新完成");
        }

        private async Task RequestWorldStateAsync()
        {
            if (IsRequestingState)
            {
                return;
            }

            WriteMessage("正在请求世界状态...");
            await _stateProvider.CacheWorldStateAsync();
        }

        private async Task BeginLoopWorldStateAsync()
        {
            await RequestWorldStateAsync();
            if (!_stateTimer.IsEnabled)
            {
                _stateTimer.Start();
            }
        }
    }
}
