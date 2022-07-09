// Copyright (c) Richasy. All rights reserved.

using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 应用搜索框.
    /// </summary>
    public sealed partial class AppSearchBox : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSearchBox"/> class.
        /// </summary>
        public AppSearchBox()
        {
            InitializeComponent();
        }

        private void OnSearchBoxSubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
        }
    }
}
