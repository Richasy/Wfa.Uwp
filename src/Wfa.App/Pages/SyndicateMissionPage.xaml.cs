// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 赏金任务页面.
    /// </summary>
    public sealed partial class SyndicateMissionPage : SyndicateMissionPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicateMissionPage"/> class.
        /// </summary>
        public SyndicateMissionPage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SyndicateMissionPage"/> 的基类.
    /// </summary>
    public class SyndicateMissionPageBase : AppPage<SyndicateMissionPageViewModel>
    {
    }
}
