// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Models.Market;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.MarketItems;

using static Wfa.Models.Data.Constants.AppConstants.Market;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 条目订单页面视图模型.
    /// </summary>
    public sealed partial class ItemOrderPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderPageViewModel"/> class.
        /// </summary>
        public ItemOrderPageViewModel(
            IResourceToolkit resourceToolkit,
            IMarketProvider marketProvider)
        {
            _resourceToolkit = resourceToolkit;
            _marketProvider = marketProvider;
            Orders = new ObservableCollection<ItemOrderViewModel>();
            OrderTypeCollection = new ObservableCollection<KeyValue>();
            UserStatusCollection = new ObservableCollection<KeyValue>();
            SortTypeCollection = new ObservableCollection<KeyValue>();

            AddFilter(OrderTypeCollection, Seller, LanguageNames.Seller);
            AddFilter(OrderTypeCollection, Buyer, LanguageNames.Buyer);

            AddFilter(SortTypeCollection, PriceAscending, LanguageNames.PriceAscending);
            AddFilter(SortTypeCollection, PriceDescending, LanguageNames.PriceDescending);
            AddFilter(SortTypeCollection, CountAscending, LanguageNames.CountAscending);
            AddFilter(SortTypeCollection, CountDescending, LanguageNames.CountDescending);

            AddFilter(UserStatusCollection, InGame, LanguageNames.InGame);
            AddFilter(UserStatusCollection, Online, LanguageNames.Online);
            AddFilter(UserStatusCollection, Offline, LanguageNames.Offline);

            CurrentOrderType = OrderTypeCollection.First();
            CurrentSortType = SortTypeCollection.First();
            CurrentUserStatus = UserStatusCollection.First();

            ActiveCommand = ReactiveCommand.CreateFromTask(ActiveAsync);
            DeactiveCommand = ReactiveCommand.Create(Deactive);
            FilterCommand = ReactiveCommand.Create(Filter);

            _isLoading = ActiveCommand.IsExecuting.ToProperty(this, x => x.IsLoading);

            ActiveCommand.ThrownExceptions
                .Merge(FilterCommand.ThrownExceptions)
                .Subscribe(LogException);

            this.WhenAnyValue(x => x.CurrentOrderType, x => x.CurrentSortType, x => x.CurrentUserStatus)
                .Subscribe(_ => Filter());
        }

        /// <summary>
        /// 设置数据.
        /// </summary>
        /// <param name="data">条目数据.</param>
        public void SetData(MarketItem data) => Item = data;

        private async Task ActiveAsync()
        {
            TryClear(Orders);
            _orders = (await _marketProvider.GetItemOrdersAsync(Item.Identifier)).ToList();
            Filter();
        }

        private void Deactive()
        {
            Item = null;
            TryClear(Orders);
        }

        private void Filter()
        {
            if (!_orders?.Any() ?? true)
            {
                return;
            }

            TryClear(Orders);
            var isSell = CurrentOrderType.Key == Seller;
            var orderType = isSell ? "sell" : "buy";
            var orders = _orders.Where(p => p.OrderType == orderType)
                                .Where(p => p.User.Status == CurrentUserStatus.Key);

            if (CurrentSortType.Key == PriceAscending)
            {
                orders = orders.OrderBy(p => p.Platinum);
            }
            else if (CurrentOrderType.Key == PriceDescending)
            {
                orders = orders.OrderByDescending(p => p.Platinum);
            }
            else if (CurrentOrderType.Key == CountAscending)
            {
                orders = orders.OrderBy(p => p.Quantity);
            }
            else if (CurrentOrderType.Key == CountDescending)
            {
                orders = orders.OrderByDescending(p => p.Quantity);
            }

            orders.ToList().ForEach(p => Orders.Add(new ItemOrderViewModel(p, Item)));
            IsEmpty = Orders.Count == 0;
        }

        private void AddFilter(ObservableCollection<KeyValue> collection, string key, LanguageNames value)
            => collection.Add(new KeyValue(key, _resourceToolkit.GetLocaleString(value)));
    }
}
