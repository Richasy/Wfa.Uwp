// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 资料库详情视图模型.
    /// </summary>
    public sealed partial class LibraryDetailPageViewModel
    {
        private readonly IResourceToolkit _resourceToolkit;
        private readonly ICommunityProvider _communityProvider;
        private readonly LibraryDbContext _dbContext;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;

        private CommunityDataType _type;

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 条目集合.
        /// </summary>
        public ObservableCollection<LibraryItemViewModel> Items { get; }

        /// <summary>
        /// 是否正在加载.
        /// </summary>
        public bool IsLoading => _isLoading.Value;

        /// <summary>
        /// 条目集合是否为空.
        /// </summary>
        [Reactive]
        public bool IsEmpty { get; set; }

        /// <summary>
        /// 标题.
        /// </summary>
        [Reactive]
        public string Title { get; set; }
    }
}
