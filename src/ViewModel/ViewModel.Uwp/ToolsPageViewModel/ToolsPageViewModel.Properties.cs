// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using Wfa.ViewModel.Tools;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 工具页面视图模型.
    /// </summary>
    public sealed partial class ToolsPageViewModel
    {
        /// <summary>
        /// 工具类型.
        /// </summary>
        public ObservableCollection<ToolItemViewModel> Tools { get; }
    }
}
