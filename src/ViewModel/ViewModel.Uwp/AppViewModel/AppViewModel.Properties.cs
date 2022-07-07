// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;
using Wfa.Models.Data.Context;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;

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
        private readonly LibraryDbContext _dbContext;

        /// <summary>
        /// 检查数据库更新的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CheckDatabaseCommand { get; }
    }
}
