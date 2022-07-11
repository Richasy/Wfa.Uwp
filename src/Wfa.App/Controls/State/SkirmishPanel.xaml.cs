// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.Items;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 前哨战面板.
    /// </summary>
    public sealed partial class SkirmishPanel : SkirmishPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkirmishPanel"/> class.
        /// </summary>
        public SkirmishPanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SkirmishPanel"/> 的基类.
    /// </summary>
    public class SkirmishPanelBase : ReactiveUserControl<SkirmishViewModel>
    {
    }
}
