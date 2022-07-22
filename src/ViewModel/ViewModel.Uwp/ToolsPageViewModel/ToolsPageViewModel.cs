// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using Splat;
using Wfa.Models.Enums;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Tools;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 工具页面视图模型.
    /// </summary>
    public sealed partial class ToolsPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsPageViewModel"/> class.
        /// </summary>
        public ToolsPageViewModel()
        {
            Tools = new ObservableCollection<ToolItemViewModel>();
            AddTool(ToolType.Translate);
            AddTool(ToolType.FriendLinks);
            AddTool(ToolType.BiliSearch);
        }

        private void AddTool(ToolType type)
        {
            var toolItem = Locator.Current.GetService<ToolItemViewModel>();
            toolItem.SetType(type);
            Tools.Add(toolItem);
        }
    }
}
