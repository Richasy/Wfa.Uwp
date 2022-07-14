// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Wfa.ViewModel.MarketItems;

namespace Wfa.App.Controls.Market
{
    /// <summary>
    /// 商店建议条目.
    /// </summary>
    public sealed partial class SuggestItem : SuggestItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestItem"/> class.
        /// </summary>
        public SuggestItem() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SuggestItem"/> 的基类.
    /// </summary>
    public class SuggestItemBase : ReactiveUserControl<MarketSuggestionItemViewModel>
    {
    }
}
