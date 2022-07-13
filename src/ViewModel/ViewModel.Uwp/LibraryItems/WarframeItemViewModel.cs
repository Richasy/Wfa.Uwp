// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Models.Market;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Windows.System;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// 战甲条目视图模型.
    /// </summary>
    public sealed class WarframeItemViewModel : ViewModelBase, IDataInitializeViewModel<Warframe>
    {
        private readonly LibraryDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarframeItemViewModel"/> class.
        /// </summary>
        public WarframeItemViewModel(LibraryDbContext dbContext)
        {
            Abilities = new ObservableCollection<WarframeAbility>();
            MarketItems = new ObservableCollection<MarketItem>();
            _dbContext = dbContext;
            InitializeCommand = ReactiveCommand.CreateFromTask<Warframe>(InitializeAsync);
            OpenWikiCommand = ReactiveCommand.CreateFromTask(OpenWikiAsync);
            JumpToMarketCommand = ReactiveCommand.Create<MarketItem>(JumpToMarket);
        }

        /// <summary>
        /// 初始化命令.
        /// </summary>
        public ReactiveCommand<Warframe, Unit> InitializeCommand { get; }

        /// <summary>
        /// 打开维基的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenWikiCommand { get; }

        /// <summary>
        /// 跳转到市场的命令.
        /// </summary>
        public ReactiveCommand<MarketItem, Unit> JumpToMarketCommand { get; }

        /// <summary>
        /// 战甲技能.
        /// </summary>
        public ObservableCollection<WarframeAbility> Abilities { get; }

        /// <summary>
        /// 相关的商店条目.
        /// </summary>
        public ObservableCollection<MarketItem> MarketItems { get; }

        /// <summary>
        /// 战甲属性最大值.
        /// </summary>
        [Reactive]
        public double PropertyMaxValue { get; set; }

        /// <summary>
        /// 战甲数据.
        /// </summary>
        [Reactive]
        public Warframe Data { get; set; }

        /// <summary>
        /// 是否有被动技能.
        /// </summary>
        [Reactive]
        public bool HasPassiveDescription { get; set; }

        /// <summary>
        /// 是否有维基链接.
        /// </summary>
        [Reactive]
        public bool HasWikiLink { get; set; }

        /// <summary>
        /// 是否有关联的商店条目.
        /// </summary>
        [Reactive]
        public bool HasMarketItems { get; set; }

        private async Task InitializeAsync(Warframe data)
        {
            Data = data;
            HasPassiveDescription = !string.IsNullOrEmpty(data.PassiveDescription);
            HasWikiLink = !string.IsNullOrEmpty(data.WikiaUrl);
            PropertyMaxValue = new double[] { data.Armor, data.Health, data.Power, data.Shield }.Max();
            TryClear(Abilities);
            if (data.Abilities?.Any() ?? false)
            {
                data.Abilities.ForEach(p => Abilities.Add(p));
            }

            var marketItems = await _dbContext.MarketItems.Where(p => p.Name.Contains(data.Name)).ToListAsync();
            HasMarketItems = marketItems.Count > 0;
            if (HasMarketItems)
            {
                marketItems.ForEach(p => MarketItems.Add(p));
            }
        }

        private Task OpenWikiAsync()
            => Launcher.LaunchUriAsync(new Uri(Data.WikiaUrl)).AsTask();

        private void JumpToMarket(MarketItem item)
        {
            var navVM = Locator.Current.GetService<NavigationViewModel>();
            navVM.NavigateToSecondaryView(Models.Enums.PageIds.MarketItemOrder, item);
        }
    }
}
