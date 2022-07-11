// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;

namespace Wfa.ViewModel.Interfaces
{
    /// <summary>
    /// 包含激活与注销功能的视图模型.
    /// </summary>
    public interface IPageViewModel
    {
        /// <summary>
        /// 激活命令.
        /// </summary>
        ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <summary>
        /// 注销命令.
        /// </summary>
        ReactiveCommand<Unit, Unit> DeactiveCommand { get; }
    }
}
