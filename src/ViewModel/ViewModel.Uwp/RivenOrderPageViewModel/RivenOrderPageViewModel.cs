// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Wfa.Models.Data.Context;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Models.Market;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.MarketItems;
using Windows.System;
using static Wfa.Models.Data.Constants.AppConstants.Market;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 紫卡订单页面视图模型.
    /// </summary>
    public sealed partial class RivenOrderPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderPageViewModel"/> class.
        /// </summary>
        public RivenOrderPageViewModel(
            IResourceToolkit resourceToolkit,
            IMarketProvider marketProvider,
            LibraryDbContext dbContext)
        {
            _resourceToolkit = resourceToolkit;
            _marketProvider = marketProvider;
            _dbContext = dbContext;
            Orders = new ObservableCollection<RivenOrderViewModel>();
            OrderTypeCollection = new ObservableCollection<KeyValue>();
            SortTypeCollection = new ObservableCollection<KeyValue>();
            UserStatusCollection = new ObservableCollection<KeyValue>();
            Attributes = new ObservableCollection<RivenAttribute>();

            AddFilter(OrderTypeCollection, Buyout, LanguageNames.Buyout);
            AddFilter(OrderTypeCollection, Auction, LanguageNames.Auction);

            AddFilter(SortTypeCollection, PriceAscending, LanguageNames.PriceAscending);
            AddFilter(SortTypeCollection, PriceDescending, LanguageNames.PriceDescending);
            AddFilter(SortTypeCollection, PositiveAscending, LanguageNames.PositiveAscending);
            AddFilter(SortTypeCollection, PositiveDescending, LanguageNames.PositiveDescending);

            AddFilter(UserStatusCollection, InGame, LanguageNames.InGame);
            AddFilter(UserStatusCollection, Online, LanguageNames.Online);
            AddFilter(UserStatusCollection, Offline, LanguageNames.Offline);

            CurrentOrderType = OrderTypeCollection.First();
            CurrentSortType = SortTypeCollection.First();
            CurrentUserStatus = UserStatusCollection.First();

            ActiveCommand = ReactiveCommand.CreateFromTask(ActiveAsync);
            DeactiveCommand = ReactiveCommand.Create(Deactive);
            OpenWMCommand = ReactiveCommand.CreateFromTask(OpenWarframeMarketAsync);

            _isLoading = ActiveCommand.IsExecuting.ToProperty(this, x => x.IsLoading);

            ActiveCommand.ThrownExceptions
                .Subscribe(LogException);

            this.WhenAnyValue(x => x.CurrentOrderType, x => x.CurrentSortType, x => x.CurrentAttribute)
                .Select(_ => Unit.Default)
                .InvokeCommand(ActiveCommand);

            this.WhenAnyValue(x => x.CurrentUserStatus)
                .Subscribe(_ => Filter());
        }

        /// <summary>
        /// 设置数据.
        /// </summary>
        /// <param name="data">条目数据.</param>
        public void SetData(RivenWeapon data) => Item = data;

        private async Task ActiveAsync()
        {
            TryClear(Orders);
            IsAttributeEnabled = CurrentAttribute != null && CurrentAttribute?.Identifier != "none";

            if (Attributes.Count == 0)
            {
                // 初始化增益属性.
                var attributes = await _dbContext.RivenAttributes.ToListAsync();
                attributes.Where(p => !p.IsSearchOnly && !p.IsNegativeOnly).ToList().ForEach(p => Attributes.Add(p));
                Attributes.Insert(0, new RivenAttribute
                {
                    Id = "x",
                    Identifier = "none",
                    Effect = "None",
                });

                CurrentAttribute = Attributes.First();
                return;
            }

            if (Item == null)
            {
                return;
            }

            var itemIdentifier = Item.Identifier;
            var buyoutPolicy = CurrentOrderType.Key == Buyout ? MarketBuyoutPolicy.DirectSale : MarketBuyoutPolicy.Auction;
            var positiveAttribute = CurrentAttribute.Identifier == "none" ? string.Empty : CurrentAttribute.Identifier;
            var sortPolicy = CurrentSortType.Key switch
            {
                PriceDescending => MarketSortType.PriceDescending,
                PositiveAscending => MarketSortType.PositiveAttributeAscending,
                PositiveDescending => MarketSortType.PositiveAttributeDescending,
                _ => MarketSortType.PriceAscending,
            };
            var orders = await _marketProvider.GetRivenOrdersAsync(itemIdentifier, buyoutPolicy, new[] { positiveAttribute }, default, default, default, default, default, RivenRankType.All, RivenModPolarity.Any, sortPolicy);
            _orders = orders.ToList();
            Filter();
        }

        private void Filter()
        {
            if (Item == null || _orders == null)
            {
                return;
            }

            TryClear(Orders);
            if (_orders.Count == 0)
            {
                IsEmpty = true;
                return;
            }

            var orders = _orders.Where(p => p.Owner.Status == CurrentUserStatus.Key);
            foreach (var item in orders)
            {
                var order = new RivenOrderViewModel(item, Attributes.ToList(), Item);
                Orders.Add(order);
            }

            IsEmpty = Orders.Count == 0;
        }

        private void Deactive()
        {
            Item = null;
            TryClear(Orders);
        }

        private void AddFilter(ObservableCollection<KeyValue> collection, string key, LanguageNames value)
            => collection.Add(new KeyValue(key, _resourceToolkit.GetLocaleString(value)));

        private async Task OpenWarframeMarketAsync()
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/auctions/search?type=riven&weapon_url_name={Item.Identifier}&polarity=any&sort_by=price_desc"));
    }
}
