// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.State;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 事件条目视图模型.
    /// </summary>
    public sealed class EventItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventItemViewModel"/> class.
        /// </summary>
        /// <param name="event">活动信息.</param>
        public EventItemViewModel(Event @event)
        {
            UpdateData(@event);
            UpdateDataCommand = ReactiveCommand.Create<Event>(UpdateData);
        }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<Event, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 当前进度.
        /// </summary>
        [Reactive]
        public string Progress { get; set; }

        /// <summary>
        /// 过期时间.
        /// </summary>
        [Reactive]
        public string ExpiryDate { get; set; }

        /// <summary>
        /// 数据源.
        /// </summary>
        [Reactive]
        public Event Data { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is EventItemViewModel model && EqualityComparer<Event>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void UpdateData(Event @event)
        {
            Progress = @event.Progress.ToString("0.0");

            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            var format = resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.ExpiryDateFormat);
            ExpiryDate = string.Format(format, @event.ExpiryTime.Humanize());
            Data = @event;
        }
    }
}
