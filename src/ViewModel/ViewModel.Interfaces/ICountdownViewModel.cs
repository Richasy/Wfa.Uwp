// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;

namespace Wfa.ViewModel.Interfaces
{
    /// <summary>
    /// 具备倒计时的视图模型.
    /// </summary>
    public interface ICountdownViewModel
    {
        /// <summary>
        /// 倒计时.
        /// </summary>
        string Countdown { get; set; }

        /// <summary>
        /// 更新倒计时命令.
        /// </summary>
        ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }
    }
}
