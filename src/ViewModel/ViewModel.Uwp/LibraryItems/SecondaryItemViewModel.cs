// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// 次要武器视图模型.
    /// </summary>
    public sealed class SecondaryItemViewModel : GunViewModelBase<Secondary>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecondaryItemViewModel"/> class.
        /// </summary>
        public SecondaryItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel,
            IResourceToolkit resourceToolkit)
            : base(dbContext, navigationViewModel, resourceToolkit)
        {
        }
    }
}
