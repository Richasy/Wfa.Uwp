// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.Items;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 裂缝条目.
    /// </summary>
    public sealed partial class FissureItem : FissureItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FissureItem"/> class.
        /// </summary>
        public FissureItem() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="FissureItem"/> 的基类.
    /// </summary>
    public class FissureItemBase : ReactiveUserControl<FissureItemViewModel>
    {
    }
}
