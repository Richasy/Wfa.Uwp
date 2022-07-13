// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.Models.State;
using Wfa.ViewModel.StateItems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 战舰建造面板.
    /// </summary>
    public sealed partial class ConstructionPanel : ConstructionPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructionPanel"/> class.
        /// </summary>
        public ConstructionPanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="ConstructionPanel"/> 的基类.
    /// </summary>
    public class ConstructionPanelBase : ReactiveUserControl<ConstructionProgressViewModel>
    {
    }
}
