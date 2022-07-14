// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 警报视图模型.
    /// </summary>
    public sealed class AlertViewModel : ViewModelBase, ICountdownViewModel
    {
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertViewModel"/> class.
        /// </summary>
        public AlertViewModel(Alert data)
        {
            UpdateData(data);
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<Alert>(UpdateData);
        }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<Alert, Unit> UpdateDataCommand { get; }

        /// <inheritdoc/>
        [Reactive]
        public string Countdown { get; set; }

        private void UpdateData(Alert data)
        {
            if (data == null)
            {
                return;
            }

            _expiryTime = data.ExpiryTime.ToLocalTime();
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            if (_expiryTime == DateTime.MinValue
                || _expiryTime <= DateTime.Now)
            {
                Countdown = string.Empty;
                return;
            }

            var endFormat = Locator.Current.GetService<IResourceToolkit>().GetLocaleString(LanguageNames.AlertFormatText);
            Countdown = string.Format(endFormat, _expiryTime.Humanize());
        }
    }
}
