// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Context;
using Wfa.Models.Data.Local;
using Wfa.Models.Market;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.MarketItems;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 商店搜索框视图模型.
    /// </summary>
    public sealed partial class MarketSearchBoxViewModel
    {
        private readonly LibraryDbContext _dbContext;
        private readonly NavigationViewModel _navigationViewModel;
        private readonly IResourceToolkit _resourceToolkit;
        private readonly List<MarketItem> _marketItems;
        private readonly List<LichWeapon> _lichWeapons;
        private readonly List<RivenWeapon> _rivenWeapons;

        /// <summary>
        /// 搜索标头.
        /// </summary>
        public ObservableCollection<MarketTypeHeader> SearchHeaders { get; }

        /// <summary>
        /// 建议列表.
        /// </summary>
        public ObservableCollection<MarketSuggestionItemViewModel> Suggestions { get; }

        /// <summary>
        /// 初始化命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InitializeCommand { get; }

        /// <summary>
        /// 打开订单页面的命令.
        /// </summary>
        public ReactiveCommand<MarketSuggestionItemViewModel, Unit> GotoOrderPageCommand { get; }

        /// <summary>
        /// 当前标头.
        /// </summary>
        [Reactive]
        public MarketTypeHeader CurrentHeader { get; set; }

        /// <summary>
        /// 搜索文本.
        /// </summary>
        [Reactive]
        public string SearchText { get; set; }
    }
}
