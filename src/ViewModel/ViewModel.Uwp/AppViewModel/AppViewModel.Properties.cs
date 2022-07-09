// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Context;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 应用视图模型.
    /// </summary>
    public sealed partial class AppViewModel
    {
        private readonly ISettingsToolkit _settingsToolkit;
        private readonly IResourceToolkit _resourceToolkit;

        private readonly ICommunityProvider _communityProvider;
        private readonly IMarketProvider _marketProvider;
        private readonly IWikiProvider _wikiProvider;
        private readonly IStateProvider _stateProvider;
        private readonly LibraryDbContext _dbContext;

        private readonly DispatcherTimer _stateTimer;
        private readonly ObservableAsPropertyHelper<bool> _isRequestingState;

        private bool? _isWide;

        /// <summary>
        /// 检查社区资料库更新的命令.
        /// </summary>
        public ReactiveCommand<bool, Unit> CheckLibraryDatabaseCommand { get; }

        /// <summary>
        /// 检查 WM 数据库是否初始化的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CheckWarframeMarketDatabaseCommand { get; }

        /// <summary>
        /// 检查维基翻译数据是否初始化的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CheckTranslateDatabaseCommand { get; }

        /// <summary>
        /// 检查维基简繁互译数据是否初始化的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CheckPatchDatabaseCommand { get; }

        /// <summary>
        /// 更新社区资料数据库的命令.
        /// </summary>
        public ReactiveCommand<string, Unit> UpdateLibraryDatabaseCommand { get; }

        /// <summary>
        /// 更新 WM 数据库的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateWarframeMarketDatabaseCommand { get; }

        /// <summary>
        /// 更新维基翻译数据的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateTranslateDatabaseCommand { get; }

        /// <summary>
        /// 更新维基简繁互译数据的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdatePatchDatabaseCommand { get; }

        /// <summary>
        /// 请求世界状态的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> RequestWorldStateCommand { get; }

        /// <summary>
        /// 开始循环请求世界状态的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> BeginLoopWorldStateCommand { get; }

        /// <summary>
        /// 是否正在请求世界状态.
        /// </summary>
        public bool IsRequestingState => _isRequestingState.Value;

        /// <summary>
        /// 导航面板是否已展开.
        /// </summary>
        [Reactive]
        public bool IsNavigatePaneOpen { get; set; }

        /// <summary>
        /// 页面横向边距.
        /// </summary>
        [Reactive]
        public Thickness PageHorizontalPadding { get; set; }

        /// <summary>
        /// 页面顶部边距.
        /// </summary>
        [Reactive]
        public Thickness PageTopPadding { get; set; }

        /// <summary>
        /// 是否显示标题栏.
        /// </summary>
        [Reactive]
        public bool IsShowTitleBar { get; set; }

        /// <summary>
        /// 是否显示菜单按钮.
        /// </summary>
        [Reactive]
        public bool IsShowMenuButton { get; set; }
    }
}
