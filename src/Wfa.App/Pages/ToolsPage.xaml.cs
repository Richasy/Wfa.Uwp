// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 工具箱页面.
    /// </summary>
    public sealed partial class ToolsPage : ToolsPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsPage"/> class.
        /// </summary>
        public ToolsPage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="ToolsPage"/> 的基类.
    /// </summary>
    public class ToolsPageBase : AppPage<ToolsPageViewModel>
    {
    }
}
