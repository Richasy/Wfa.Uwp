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
            IsShowLevel = data.BaseDrain != null;
            TotalLevel = data.BaseDrain ?? 0;
            CurrentLevel = 0;
        }

        private void ChangeDescription()
        {
            if (Data == null)
            {
                return;
            }

            if (CurrentLevel.ToString().Contains("."))
            {
                var level = CurrentLevel + 0.5;
                if (level > Data.BaseDrain)
                {
                    level = Data.BaseDrain ?? 0d;
                }

                CurrentLevel = Convert.ToInt32(level);
                return;
            }

            if (CurrentLevel == 0)
            {
                Description = Data.Description;
            }
            else
            {
                var levelDesc = Data.Effects.FirstOrDefault(p => p.Level == CurrentLevel);
                Description = levelDesc?.Description ?? Data.Description;
            }
        }
    }
}
