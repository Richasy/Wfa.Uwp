// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Local;
using Wfa.Models.Market;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.MarketItems;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 条目订单页面视图模型.
    /// </summary>
    public sealed partial class ItemOrderPageViewModel
    {
        private readonly IResourceToolkit _resourceToolkit;
        private readonly IMarketProvider _marketProvider;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private List<ItemOrder> _orders;

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 订单过滤命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> FilterCommand { get; }

        /// <summary>
        /// 订单类型集合.
        /// </summary>
        public ObservableCollection<KeyValue> OrderTypeCollection { get; }

        /// <summary>
        /// 用户状态集合.
        /// </summary>
        public ObservableCollection<KeyValue> UserStatusCollection { get; }

        /// <summary>
        /// 排序方式集合.
        /// </summary>
        public ObservableCollection<KeyValue> SortTypeCollection { get; }

        /// <summary>
        /// 订单列表.
        /// </summary>
        public ObservableCollection<ItemOrderViewModel> Orders { get; }

        /// <summary>
        /// 条目信息.
        /// </summary>
        [Reactive]
        public MarketItem Item { get; set; }

        /// <summary>
        /// 当前订单类型.
        /// </summary>
        [Reactive]
        public KeyValue CurrentOrderType { get; set; }

        /// <summary>
        /// 当前用户状态.
        /// </summary>
        [Reactive]
        public KeyValue CurrentUserStatus { get; set; }

        /// <summary>
        /// 当前排序方式.
        /// </summary>
        [Reactive]
        public KeyValue CurrentSortType { get; set; }

        /// <summary>
        /// 是否为空.
        /// </summary>
        [Reactive]
        public bool IsEmpty { get; set; }

        /// <summary>
        /// 是否正在加载.
        /// </summary>
        public bool IsLoading => _isLoading.Value;
    }
}
