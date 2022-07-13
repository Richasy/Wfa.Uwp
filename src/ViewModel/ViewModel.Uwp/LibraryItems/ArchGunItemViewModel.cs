// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// 空战枪械视图模型.
    /// </summary>
    public sealed class ArchGunItemViewModel : GunViewModelBase<ArchGun>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArchGunItemViewModel"/> class.
        /// </summary>
        public ArchGunItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel,
            IResourceToolkit resourceToolkit)
            : base(dbContext, navigationViewModel, resourceToolkit)
        {
        }
    }
}
