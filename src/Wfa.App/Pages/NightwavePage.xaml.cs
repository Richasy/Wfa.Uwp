// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 午夜电波页面.
    /// </summary>
    public sealed partial class NightwavePage : NightwavePageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NightwavePage"/> class.
        /// </summary>
        public NightwavePage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="NightwavePage"/> 的基类.
    /// </summary>
    public class NightwavePageBase : AppPage<NightwavePageViewModel>
    {
    }
}
