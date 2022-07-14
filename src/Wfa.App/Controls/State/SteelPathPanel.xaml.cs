// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.StateItems;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 钢铁之路面板.
    /// </summary>
    public sealed partial class SteelPathPanel : SteelPathPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SteelPathPanel"/> class.
        /// </summary>
        public SteelPathPanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SteelPathPanel"/> 的基类.
    /// </summary>
    public class SteelPathPanelBase : ReactiveUserControl<SteelPathViewModel>
    {
    }
}
