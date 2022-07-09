// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Wfa.Models.State;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 世界状态首页视图模型.
    /// </summary>
    /// <remarks>
    /// 应包含的模块：世界周期, 游戏新闻，商人，活动，仲裁，前哨战，战舰建造进度.
    /// </remarks>
    public sealed partial class WorldStatePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldStatePageViewModel"/> class.
        /// </summary>
        public WorldStatePageViewModel(
            IStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
            News = new ObservableCollection<NewsItemViewModel>();
            Events = new ObservableCollection<Event>();

            _stateProvider.StateChanged += OnWorldStateChanged;
            InitializeData();
        }

        private void InitializeData()
        {
            InitializeNews();
        }

        private void InitializeNews()
        {
            var news = _stateProvider.GetNews();
            if (!news?.Any() ?? true)
            {
                return;
            }

            var newCount = news.Count(p => !News.Any(j => j.Data.Equals(p)));
            if (newCount > 0)
            {
                // 有新的新闻传入，此时整体刷新.
                News.Clear();
                news.OrderByDescending(p => p.Date).ToList().ForEach(p => News.Add(new NewsItemViewModel(p)));
                NewsCount = News.Count;
            }
        }

        private void OnWorldStateChanged(object sender, EventArgs e)
            => InitializeData();
    }
}
