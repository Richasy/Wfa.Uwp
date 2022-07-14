// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 虚空裂缝页面.
    /// </summary>
    public sealed partial class FissurePage : FissurePageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FissurePage"/> class.
        /// </summary>
        public FissurePage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="FissurePage"/> 的基类.
    /// </summary>
    public class FissurePageBase : AppPage<FissurePageViewModel>
    {
    }
}
