// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.Items;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 仲裁面板.
    /// </summary>
    public sealed partial class ArbitrationPanel : ArbitrationPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArbitrationPanel"/> class.
        /// </summary>
        public ArbitrationPanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="ArbitrationPanel"/> 的基类.
    /// </summary>
    public class ArbitrationPanelBase : ReactiveUserControl<ArbitrationViewModel>
    {
    }
}
