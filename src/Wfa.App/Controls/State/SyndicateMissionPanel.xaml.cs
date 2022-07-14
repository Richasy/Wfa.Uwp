// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.StateItems;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 赏金任务面板.
    /// </summary>
    public sealed partial class SyndicateMissionPanel : SyndicateMissionPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicateMissionPanel"/> class.
        /// </summary>
        public SyndicateMissionPanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SyndicateMissionPanel"/> 的基类.
    /// </summary>
    public class SyndicateMissionPanelBase : ReactiveUserControl<SyndicateMissionViewModel>
    {
    }
}
