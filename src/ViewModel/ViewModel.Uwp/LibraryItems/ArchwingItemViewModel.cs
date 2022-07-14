// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// Archwing 信息视图模型.
    /// </summary>
    public sealed class ArchwingItemViewModel : EntryViewModelBase<Archwing>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArchwingItemViewModel"/> class.
        /// </summary>
        public ArchwingItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel)
            : base(dbContext, navigationViewModel)
            => Abilities = new ObservableCollection<ArchwingAbility>();

        /// <summary>
        /// 战甲技能.
        /// </summary>
        public ObservableCollection<ArchwingAbility> Abilities { get; }

        /// <summary>
        /// 战甲属性最大值.
        /// </summary>
        [Reactive]
        public double PropertyMaxValue { get; set; }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(Archwing data)
        {
            PropertyMaxValue = new double[] { data.Armor, data.Health, data.Power, data.Shield }.Max();
            TryClear(Abilities);
            if (data.Abilities?.Any() ?? false)
            {
                data.Abilities.ForEach(p => Abilities.Add(p));
            }

            await base.InitializeAsync(data);
        }
    }
}
