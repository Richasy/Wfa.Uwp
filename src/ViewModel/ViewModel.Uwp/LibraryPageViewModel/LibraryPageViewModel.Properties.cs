// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 资料库页面视图模型.
    /// </summary>
    public sealed partial class LibraryPageViewModel
    {
        private readonly IResourceToolkit _resourceToolkit;
        private readonly ISettingsToolkit _settingsToolkit;
        private readonly LibraryDbContext _dbContext;

        /// <summary>
        /// 分区.
        /// </summary>
        public ObservableCollection<LibrarySectionViewModel> Sections { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 最近更新时间.
        /// </summary>
        [Reactive]
        public string LastUpdateTime { get; set; }
    }
}
