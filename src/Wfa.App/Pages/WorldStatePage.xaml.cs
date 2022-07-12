// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 世界状态首页.
    /// </summary>
    public sealed partial class WorldStatePage : WorldStatePageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldStatePage"/> class.
        /// </summary>
        public WorldStatePage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="WorldStatePage"/> 的基类.
    /// </summary>
    public class WorldStatePageBase : AppPage<WorldStatePageViewModel>
    {
    }
}
