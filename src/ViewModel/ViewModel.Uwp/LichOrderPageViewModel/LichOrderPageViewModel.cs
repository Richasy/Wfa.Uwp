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
    /// 玄骸订单页面视图模型.
    /// </summary>
    public sealed partial class LichOrderPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderPageViewModel"/> class.
        /// </summary>
        public LichOrderPageViewModel(
            IResourceToolkit resourceToolkit,
            IMarketProvider marketProvider,
            LibraryDbContext dbContext)
        {
            _resourceToolkit = resourceToolkit;
            _marketProvider = marketProvider;
            _dbContext = dbContext;
            Orders = new ObservableCollection<LichOrderViewModel>();
            OrderTypeCollection = new ObservableCollection<KeyValue>();
            SortTypeCollection = new ObservableCollection<KeyValue>();
            UserStatusCollection = new ObservableCollection<KeyValue>();
            Ephemeras = new ObservableCollection<LichEphemera>();

            AddFilter(OrderTypeCollection, Buyout, LanguageNames.Buyout);
            AddFilter(OrderTypeCollection, Auction, LanguageNames.Auction);

            AddFilter(SortTypeCollection, PriceAscending, LanguageNames.PriceAscending);
            AddFilter(SortTypeCollection, PriceDescending, LanguageNames.PriceDescending);
            AddFilter(SortTypeCollection, DamageAscending, LanguageNames.DamageAscending);
            AddFilter(SortTypeCollection, DamageDescending, LanguageNames.DamageDescending);

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

            this.WhenAnyValue(x => x.CurrentOrderType, x => x.CurrentSortType, x => x.CurrentEphemera, x => x.HasEphemera)
                .Select(_ => Unit.Default)
                .InvokeCommand(ActiveCommand);

            this.WhenAnyValue(x => x.CurrentUserStatus)
                .Subscribe(_ => Filter());
        }

        /// <summary>
        /// 设置数据.
        /// </summary>
        /// <param name="data">条目数据.</param>
        public void SetData(LichWeapon data) => Item = data;

        private async Task ActiveAsync()
        {
            TryClear(Orders);
            IsEphemeraEnabled = CurrentEphemera != null && CurrentEphemera?.Identifier != "none";

            if (Ephemeras.Count == 0)
            {
                // 初始化幻纹.
                var ephemeras = await _dbContext.LichEphemeras.ToListAsync();
                ephemeras.ForEach(p => Ephemeras.Add(p));
                Ephemeras.Insert(0, new LichEphemera
                {
                    Id = "x",
                    Identifier = "none",
                    Name = "None",
                });

                CurrentEphemera = Ephemeras.First();
                return;
            }

            if (Item == null)
            {
                return;
            }

            var itemIdentifier = Item.Identifier;
            var buyoutPolicy = CurrentOrderType.Key == Buyout ? MarketBuyoutPolicy.DirectSale : MarketBuyoutPolicy.Auction;
            var elementIdentifier = CurrentEphemera.Identifier == "none" ? string.Empty : CurrentEphemera.Element;
            var sortPolicy = CurrentSortType.Key switch
            {
                PriceDescending => MarketSortType.PriceDescending,
                DamageAscending => MarketSortType.DamageAscending,
                DamageDescending => MarketSortType.DamageDescending,
                _ => MarketSortType.PriceAscending,
            };
            var orders = await _marketProvider.GetLichOrdersAsync(itemIdentifier, buyoutPolicy, elementIdentifier, HasEphemera, 0, 0, default, sortPolicy);
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
                var ephemera = string.IsNullOrEmpty(item.Item.Element)
                    ? null
                    : Ephemeras.FirstOrDefault(p => p.Element == item.Item.Element);
                var order = new LichOrderViewModel(item, ephemera);
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
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/auctions/search?type=lich&weapon_url_name={Item.Identifier}&having_ephemera=false&sort_by=price_desc"));
    }
}
