// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.StateItems;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 突击面板.
    /// </summary>
    public sealed partial class SortiePanel : SortiePanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortiePanel"/> class.
        /// </summary>
        public SortiePanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SortiePanel"/> 的基类.
    /// </summary>
    public class SortiePanelBase : ReactiveUserControl<SortieViewModel>
    {
    }
}
