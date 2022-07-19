// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.State;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 午夜电波挑战条目视图模型.
    /// </summary>
    public sealed class NightwaveItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NightwaveItemViewModel"/> class.
        /// </summary>
        public NightwaveItemViewModel(NightwaveChallenge challenge)
        {
            UpdateDataCommand = ReactiveCommand.Create<NightwaveChallenge>(UpdateData);
            UpdateData(challenge);
        }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<NightwaveChallenge, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 源数据.
        /// </summary>
        [Reactive]
        public NightwaveChallenge Data { get; set; }

        /// <summary>
        /// 任务标签.
        /// </summary>
        [Reactive]
        public string Tag { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is NightwaveItemViewModel model && EqualityComparer<NightwaveChallenge>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void UpdateData(NightwaveChallenge challenge)
        {
            Data = challenge;
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            if (challenge.IsDaily)
            {
                Tag = resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Daily);
            }
            else if (challenge.IsElite)
            {
                Tag = resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.WeeklyElite);
            }
            else
            {
                Tag = resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Weekly);
            }
        }
    }
}
