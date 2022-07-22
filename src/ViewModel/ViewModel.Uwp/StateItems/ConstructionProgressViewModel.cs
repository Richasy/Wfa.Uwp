// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.State;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 战舰建造进度视图模型.
    /// </summary>
    public sealed class ConstructionProgressViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructionProgressViewModel"/> class.
        /// </summary>
        public ConstructionProgressViewModel(ConstructionProgress data)
        {
            UpdateData(data);
            UpdateDataCommand = ReactiveCommand.Create<ConstructionProgress>(UpdateData);
        }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<ConstructionProgress, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 巨人战舰建造进度.
        /// </summary>
        [Reactive]
        public double FomorianProgress { get; set; }

        /// <summary>
        /// 利刃豺狼建造进度.
        /// </summary>
        [Reactive]
        public double RazorbackProgress { get; set; }

        /// <summary>
        /// 巨人战舰建造进度的约数文本.
        /// </summary>
        [Reactive]
        public string FomorianProgressText { get; set; }

        /// <summary>
        /// 利刃豺狼建造进度的约数文本.
        /// </summary>
        [Reactive]
        public string RazaorbackProgressText { get; set; }

        private void UpdateData(ConstructionProgress data)
        {
            FomorianProgress = Convert.ToDouble(data.FomorianProgress);
            RazorbackProgress = Convert.ToDouble(data.RazorbackProgress);

            if (FomorianProgress > 100)
            {
                FomorianProgress = 100d;
            }

            if (RazorbackProgress > 100)
            {
                RazorbackProgress = 100d;
            }

            FomorianProgressText = FomorianProgress.ToString("0");
            RazaorbackProgressText = RazorbackProgress.ToString("0");
        }
    }
}
