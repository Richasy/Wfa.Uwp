// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.Items;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 入侵条目.
    /// </summary>
    public sealed partial class InvasionItem : InvasionItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvasionItem"/> class.
        /// </summary>
        public InvasionItem() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="InvasionItem"/> 的基类.
    /// </summary>
    public class InvasionItemBase : ReactiveUserControl<InvasionItemViewModel>
    {
    }
}
