// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Models.Market;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.MarketItems;
using Windows.Globalization;
using Windows.System;

namespace Wfa.ViewModel.Base
{
    /// <summary>
    /// 资料库条目视图模型基类.
    /// </summary>
    public class EntryViewModelBase : ViewModelBase
    {
        /// <summary>
        /// 跳转到市场的命令.
        /// </summary>
        public ReactiveCommand<MarketItem, Unit> JumpToMarketCommand { get; protected set; }

        /// <summary>
        /// 相关的商店条目.
        /// </summary>
        public ObservableCollection<MarketSuggestionItemViewModel> MarketItems { get; protected set; }

        /// <summary>
        /// 是否有关联的商店条目.
        /// </summary>
        [Reactive]
        public bool HasMarketItems { get; set; }
    }

    /// <summary>
    /// 资料库条目视图模型基类.
    /// </summary>
    /// <typeparam name="T">资料库数据类型.</typeparam>
    public class EntryViewModelBase<T> : EntryViewModelBase, IDataInitializeViewModel<T>
        where T : EntryBase
    {
        private readonly LibraryDbContext _dbContext;
        private readonly NavigationViewModel _navigationViewModel;
        private string _wikiUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryViewModelBase{T}"/> class.
        /// </summary>
        public EntryViewModelBase(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel)
        {
            MarketItems = new ObservableCollection<MarketSuggestionItemViewModel>();
            _dbContext = dbContext;
            _navigationViewModel = navigationViewModel;
            InitializeCommand = ReactiveCommand.CreateFromTask<T>(InitializeAsync);
            OpenWikiCommand = ReactiveCommand.CreateFromTask(OpenWikiAsync);
            JumpToMarketCommand = ReactiveCommand.Create<MarketItem>(JumpToMarket);
        }

        /// <summary>
        /// 初始化命令.
        /// </summary>
        public ReactiveCommand<T, Unit> InitializeCommand { get; }

        /// <summary>
        /// 打开维基的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenWikiCommand { get; }

        /// <summary>
        /// 数据.
        /// </summary>
        [Reactive]
        public T Data { get; set; }

        /// <summary>
        /// 初始化数据.
        /// </summary>
        /// <param name="data">源数据.</param>
        /// <returns><see cref="Task"/>.</returns>
        protected virtual async Task InitializeAsync(T data)
        {
            Data = data;
            InitializeWikiUrl();
            var marketItems = await _dbContext.MarketItems.Where(p => p.Name.Contains(data.Name)).ToListAsync();
            HasMarketItems = marketItems.Count > 0;
            if (HasMarketItems)
            {
                marketItems.ForEach(p => MarketItems.Add(new MarketSuggestionItemViewModel(p)));
            }
        }

        private Task OpenWikiAsync()
            => Launcher.LaunchUriAsync(new Uri(_wikiUrl)).AsTask();

        private void JumpToMarket(MarketItem item)
            => _navigationViewModel.NavigateToSecondaryView(PageIds.MarketItemOrder, item);

        private void InitializeWikiUrl()
        {
            var settingsToolkit = Locator.Current.GetService<ISettingsToolkit>();
            var defaultWiki = ApplicationLanguages.Languages.First().Contains("zh", StringComparison.OrdinalIgnoreCase)
                ? WikiType.Huiji
                : WikiType.Fandom;
            var preferWiki = settingsToolkit.ReadLocalSetting(SettingNames.PreferWiki, defaultWiki);
            var prefix = preferWiki == WikiType.Huiji
                ? "https://warframe.huijiwiki.com/wiki/"
                : "https://warframe.fandom.com/wiki/";
            var itemName = preferWiki == WikiType.Huiji && HasChinese(Data.Name)
                ? Uri.EscapeDataString(Data.Name.Replace(" ", string.Empty))
                : Uri.EscapeDataString(Data.Name);

            // 如果条目名是中文，并且首选 Wiki 是 Fandom，那么就尝试直接调用数据自带的维基链接.
            _wikiUrl = preferWiki == WikiType.Fandom && HasChinese(Data.Name) && !string.IsNullOrEmpty(Data.WikiaUrl)
                ? Data.WikiaUrl
                : $"{prefix}{itemName}";
        }

        private bool HasChinese(string str)
            => Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
    }
}
