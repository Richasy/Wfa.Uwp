// Copyright (c) Richasy. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// Mod 条目视图模型.
    /// </summary>
    public sealed class ModItemViewModel : EntryViewModelBase<Mod>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModItemViewModel"/> class.
        /// </summary>
        public ModItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel)
            : base(dbContext, navigationViewModel)
        {
            this.WhenAnyValue(x => x.CurrentLevel)
                .WhereNotNull()
                .Subscribe(_ => ChangeDescription());
        }

        /// <summary>
        /// 当前 Mod 等级.
        /// </summary>
        [Reactive]
        public int CurrentLevel { get; set; }

        /// <summary>
        /// 总级数.
        /// </summary>
        [Reactive]
        public int TotalLevel { get; set; }

        /// <summary>
        /// 是否显示等级.
        /// </summary>
        [Reactive]
        public bool IsShowLevel { get; set; }

        /// <summary>
        /// Mod 描述.
        /// </summary>
        [Reactive]
        public string Description { get; set; }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(Mod data)
        {
            await base.InitializeAsync(data);
            CurrentLevel = -1;
            IsShowLevel = data.FusionLimit > 0;
            TotalLevel = data.FusionLimit;
            Description = Data.Effects.FirstOrDefault(p => p.Level == CurrentLevel)?.Description ?? string.Empty;
            CurrentLevel = 0;
        }

        private void ChangeDescription()
        {
            if (Data == null)
            {
                return;
            }

            var level = CurrentLevel == -1 ? 0 : CurrentLevel;

            Description = Data.Effects.FirstOrDefault(p => p.Level == level)?.Description ?? string.Empty;
        }
    }
}
