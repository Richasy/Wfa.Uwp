// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// 主要武器视图模型.
    /// </summary>
    public sealed class PrimaryItemViewModel : GunViewModelBase<Primary>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryItemViewModel"/> class.
        /// </summary>
        public PrimaryItemViewModel(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel,
            IResourceToolkit resourceToolkit)
            : base(dbContext, navigationViewModel, resourceToolkit)
        {
        }
    }
}
