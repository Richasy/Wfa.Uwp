// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// 空战近战武器视图模型.
    /// </summary>
    public sealed class MeleeItemViewModel : MeleeViewModelBase<Melee>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeleeItemViewModel"/> class.
        /// </summary>
        public MeleeItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel,
            IResourceToolkit resourceToolkit)
            : base(dbContext, navigationViewModel, resourceToolkit)
        {
        }
    }
}
