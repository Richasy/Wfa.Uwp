// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Wfa.App.Controls.App;
using Wfa.Models.State;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 虚空商人物品视图.
    /// </summary>
    public sealed partial class VoidTraderItemsView : CenterPopup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTraderItemsView"/> class.
        /// </summary>
        public VoidTraderItemsView() => InitializeComponent();

        /// <summary>
        /// 显示.
        /// </summary>
        /// <param name="items">所需条目列表.</param>
        public void Show(IEnumerable<VoidTraderItem> items)
        {
            Repeater.ItemsSource = items;
            Show();
        }
    }
}
