// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Models.Market;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.MarketItems;
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
        /// 是否有维基链接.
        /// </summary>
        [Reactive]
        public bool HasWikiLink { get; set; }

        /// <summary>
        /// 初始化数据.
        /// </summary>
        /// <param name="data">源数据.</param>
        /// <returns><see cref="Task"/>.</returns>
        protected virtual async Task InitializeAsync(T data)
        {
            Data = data;
            HasWikiLink = !string.IsNullOrEmpty(data.WikiaUrl);

            var marketItems = await _dbContext.MarketItems.Where(p => p.Name.Contains(data.Name)).ToListAsync();
            HasMarketItems = marketItems.Count > 0;
            if (HasMarketItems)
            {
                marketItems.ForEach(p => MarketItems.Add(new MarketSuggestionItemViewModel(p)));
            }
        }

        private Task OpenWikiAsync()
            => Launcher.LaunchUriAsync(new Uri(Data.WikiaUrl)).AsTask();

        private void JumpToMarket(MarketItem item)
            => _navigationViewModel.NavigateToSecondaryView(Models.Enums.PageIds.MarketItemOrder, item);
    }
}
