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
            LibraryDbContext dbContext)
        {
            _settingsToolkit = settingsToolkit;
            _resourceToolkit = resourceToolkit;

            _communityProvider = communityProvider;
            _marketProvider = marketProvider;
            _dbContext = dbContext;

            CheckLibraryDatabaseCommand = ReactiveCommand.CreateFromTask(CheckLibraryDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            CheckWarframeMarketDatabaseCommand = ReactiveCommand.CreateFromTask(CheckWaframeMarketDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);
            UpdateWarframeMarketDatabaseCommand = ReactiveCommand.CreateFromTask(UpdateWarframeMarketDatabaseAsync, outputScheduler: RxApp.MainThreadScheduler);

            CheckLibraryDatabaseCommand.ThrownExceptions
                .Merge(CheckWarframeMarketDatabaseCommand.ThrownExceptions)
                .Merge(UpdateWarframeMarketDatabaseCommand.ThrownExceptions)
                .Subscribe(LogException);
        }

        /// <summary>
        /// 初始化语言选项.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public async Task InitializeLanguageAsync()
        {
            var appLanguage = ApplicationLanguages.Languages.First();
            var supportLan = "en";
            if (appLanguage.Contains("zh", System.StringComparison.OrdinalIgnoreCase))
            {
                supportLan = appLanguage.Contains("cn", StringComparison.OrdinalIgnoreCase) || appLanguage.Contains("hans", StringComparison.OrdinalIgnoreCase)
                    ? "zh"
                    : "tc";
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

        private async Task CheckLibraryDatabaseAsync()
        {
            var communityUpdateCheckResult = await _communityProvider.CheckUpdateAsync();
            if (communityUpdateCheckResult.NeedUpdate)
            {
                WriteMessage($"社区资料库需要更新，远端版本：{communityUpdateCheckResult.RemoteVersion}");
                WriteMessage($"开始缓存所需文件...");

                // TODO: 显示更新UI.
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
                await _communityProvider.CommitLibraryVersionAsync(communityUpdateCheckResult.RemoteVersion);
                WriteMessage("缓存已清理，记录此次更新 Id");
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

        private async Task UpdateWarframeMarketDatabaseAsync()
        {
            WriteMessage("开始更新WM条目内容");
            await _marketProvider.UpdateMarketItemsAsync();
            WriteMessage("WM 条目内容更新完成");
        }
    }
}
