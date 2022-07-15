// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 设置页面视图模型.
    /// </summary>
    public sealed partial class SettingsPageViewModel
    {
        private readonly IAppToolkit _appToolkit;
        private readonly ISettingsToolkit _settingsToolkit;
        private readonly IResourceToolkit _resourceToolkit;
        private readonly AppViewModel _appViewModel;
        private readonly LibraryDbContext _dbContext;
        private string _initializeTheme;

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 维基类型集合.
        /// </summary>
        public ObservableCollection<WikiType> WikiTypes { get; }

        /// <summary>
        /// 应用主题.
        /// </summary>
        [Reactive]
        public string AppTheme { get; set; }

        /// <summary>
        /// 是否显示主题重启提示文本.
        /// </summary>
        [Reactive]
        public bool IsShowThemeRestartTip { get; set; }

        /// <summary>
        /// 是否预启动.
        /// </summary>
        [Reactive]
        public bool IsPrelaunch { get; set; }

        /// <summary>
        /// 是否开机自启动.
        /// </summary>
        [Reactive]
        public bool IsStartup { get; set; }

        /// <summary>
        /// 启动项警告文本.
        /// </summary>
        [Reactive]
        public string StartupWarningText { get; set; }

        /// <summary>
        /// 应用版本.
        /// </summary>
        [Reactive]
        public string Version { get; set; }

        /// <summary>
        /// 社区数据库更新时间.
        /// </summary>
        [Reactive]
        public string CommunityDatabaseUpdateTime { get; set; }

        /// <summary>
        /// 市场数据库更新时间.
        /// </summary>
        [Reactive]
        public string MarketDatabaseUpdateTime { get; set; }

        /// <summary>
        /// 偏好的维基.
        /// </summary>
        [Reactive]
        public WikiType PreferWiki { get; set; }
    }
}
