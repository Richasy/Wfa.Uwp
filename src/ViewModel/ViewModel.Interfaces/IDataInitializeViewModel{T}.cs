// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;

namespace Wfa.ViewModel.Interfaces
{
    /// <summary>
    /// 包含数据初始化功能的视图模型.
    /// </summary>
    /// <typeparam name="T">传入的数据类型.</typeparam>
    public interface IDataInitializeViewModel<T>
    {
        /// <summary>
        /// 初始化命令.
        /// </summary>
        public ReactiveCommand<T, Unit> InitializeCommand { get; }
    }
}
