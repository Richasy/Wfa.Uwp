// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.StateItems;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 虚空商人面板.
    /// </summary>
    public sealed partial class VoidTraderPanel : VoidTraderPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTraderPanel"/> class.
        /// </summary>
        public VoidTraderPanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="VoidTraderPanel"/> 的基类.
    /// </summary>
    public class VoidTraderPanelBase : ReactiveUserControl<VoidTraderViewModel>
    {
    }
}
