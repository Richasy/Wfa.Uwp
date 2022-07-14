// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Wfa.Models.Data.Context;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Models.Market;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.MarketItems;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 商店搜索框视图模型.
    /// </summary>
    public sealed partial class MarketSearchBoxViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketSearchBoxViewModel"/> class.
        /// </summary>
        public MarketSearchBoxViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel,
            IResourceToolkit resourceToolkit)
        {
            _dbContext = dbContext;
            _navigationViewModel = navigationViewModel;
            _resourceToolkit = resourceToolkit;
            _marketItems = new List<MarketItem>();
            _lichWeapons = new List<LichWeapon>();
            _rivenWeapons = new List<RivenWeapon>();

            Suggestions = new ObservableCollection<MarketSuggestionItemViewModel>();
            SearchHeaders = new ObservableCollection<MarketTypeHeader>
            {
                new MarketTypeHeader(MarketDataType.Items, _resourceToolkit.GetLocaleString(LanguageNames.Items)),
                new MarketTypeHeader(MarketDataType.LichWeapons, _resourceToolkit.GetLocaleString(LanguageNames.Lich)),
                new MarketTypeHeader(MarketDataType.RivenWeapons, _resourceToolkit.GetLocaleString(LanguageNames.Riven)),
            };
            CurrentHeader = SearchHeaders.First();

            InitializeCommand = ReactiveCommand.CreateFromTask(InitializeAsync);
            GotoOrderPageCommand = ReactiveCommand.Create<MarketSuggestionItemViewModel>(GotoOrderPage);

            this.WhenAnyValue(p => p.SearchText)
                .Subscribe(Search);
        }

        private async Task InitializeAsync()
        {
            TryClear(Suggestions);
            TryClear(_marketItems);
            TryClear(_lichWeapons);
            TryClear(_rivenWeapons);

            var items = await _dbContext.MarketItems.ToListAsync();
            items.ForEach(p => _marketItems.Add(p));

            var lichWeapons = await _dbContext.LichWeapons.ToListAsync();
            lichWeapons.ForEach(p => _lichWeapons.Add(p));

            var rivenWeapons = await _dbContext.RivenWeapons.ToListAsync();
            rivenWeapons.ForEach(p => _rivenWeapons.Add(p));
        }

        private void GotoOrderPage(MarketSuggestionItemViewModel item)
        {
            switch (item.ItemType)
            {
                case MarketDataType.Items:
                    _navigationViewModel.NavigateToSecondaryView(PageIds.MarketItemOrder, item.Data);
                    break;
                case MarketDataType.LichWeapons:
                    _navigationViewModel.NavigateToSecondaryView(PageIds.MarketLichOrder, item.Data);
                    break;
                case MarketDataType.RivenWeapons:
                    _navigationViewModel.NavigateToSecondaryView(PageIds.MarketRivenOrder, item.Data);
                    break;
                default:
                    break;
            }

            SearchText = string.Empty;
        }

        private void Search(string text)
        {
            TryClear(Suggestions);
            if (string.IsNullOrEmpty(text) || _marketItems.Count == 0)
            {
                return;
            }

            IEnumerable<ItemBase> searchResult = default;
            if (CurrentHeader.Type == MarketDataType.Items)
            {
                searchResult = _marketItems.Where(p => p.Name.Contains(text, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (CurrentHeader.Type == MarketDataType.LichWeapons)
            {
                searchResult = _lichWeapons.Where(p => p.Name.Contains(text, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (CurrentHeader.Type == MarketDataType.RivenWeapons)
            {
                searchResult = _rivenWeapons.Where(p => p.Name.Contains(text, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (searchResult != null)
            {
                if (searchResult.Count() > 10)
                {
                    searchResult = searchResult.Take(10).ToList();
                }

                foreach (var item in searchResult)
                {
                    Suggestions.Add(new MarketSuggestionItemViewModel(item));
                }
            }
        }
    }
}
