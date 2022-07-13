// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.State;
using Wfa.ViewModel.Base;
using Windows.System;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 新闻条目视图模型.
    /// </summary>
    public sealed class NewsItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsItemViewModel"/> class.
        /// </summary>
        public NewsItemViewModel(News data)
        {
            Data = data;
            PublishTime = data.Date.Humanize();

            OpenInBroswerCommand = ReactiveCommand.CreateFromTask(OpenInBroswerAsync);
        }

        /// <summary>
        /// 在浏览器中打开新闻的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenInBroswerCommand { get; }

        /// <summary>
        /// 数据.
        /// </summary>
        [Reactive]
        public News Data { get; set; }

        /// <summary>
        /// 发布时间的可读文本.
        /// </summary>
        [Reactive]
        public string PublishTime { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is NewsItemViewModel model && EqualityComparer<News>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private async Task OpenInBroswerAsync()
            => await Launcher.LaunchUriAsync(new Uri(Data.Link));
    }
}
