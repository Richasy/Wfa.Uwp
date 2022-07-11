// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.Items;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 每日折扣面板.
    /// </summary>
    public sealed partial class DailySalePanel : DailySalePanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailySalePanel"/> class.
        /// </summary>
        public DailySalePanel() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="DailySalePanel"/> 的基类.
    /// </summary>
    public class DailySalePanelBase : ReactiveUserControl<DailySaleViewModel>
    {
    }
}
