// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.State;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 前哨战任务.
    /// </summary>
    public sealed class SkirmishViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkirmishViewModel"/> class.
        /// </summary>
        public SkirmishViewModel(Skirmish data)
        {
            UpdateData(data);
            UpdateDataCommand = ReactiveCommand.Create<Skirmish>(UpdateData);
        }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<Skirmish, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 数据.
        /// </summary>
        [Reactive]
        public Skirmish Data { get; set; }

        /// <summary>
        /// 是否激活.
        /// </summary>
        [Reactive]
        public bool IsActive { get; set; }

        /// <summary>
        /// 阵营.
        /// </summary>
        [Reactive]
        public string Faction { get; set; }

        /// <summary>
        /// 地点.
        /// </summary>
        [Reactive]
        public string Node { get; set; }

        private void UpdateData(Skirmish data)
        {
            Data = data;
            IsActive = data?.IsActive ?? false;
            Faction = data?.Mission?.Faction ?? "--";
            Node = data?.Mission?.Node ?? "--";
        }
    }
}
