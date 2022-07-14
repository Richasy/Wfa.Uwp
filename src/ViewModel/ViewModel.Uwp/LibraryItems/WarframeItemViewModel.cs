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
    /// 战甲条目视图模型.
    /// </summary>
    public sealed class WarframeItemViewModel : EntryViewModelBase<Warframe>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarframeItemViewModel"/> class.
        /// </summary>
        public WarframeItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel)
            : base(dbContext, navigationViewModel)
            => Abilities = new ObservableCollection<WarframeAbility>();

        /// <summary>
        /// 战甲技能.
        /// </summary>
        public ObservableCollection<WarframeAbility> Abilities { get; }

        /// <summary>
        /// 战甲属性最大值.
        /// </summary>
        [Reactive]
        public double PropertyMaxValue { get; set; }

        /// <summary>
        /// 是否有被动技能.
        /// </summary>
        [Reactive]
        public bool HasPassiveDescription { get; set; }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(Warframe data)
        {
            HasPassiveDescription = !string.IsNullOrEmpty(data.PassiveDescription);
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
