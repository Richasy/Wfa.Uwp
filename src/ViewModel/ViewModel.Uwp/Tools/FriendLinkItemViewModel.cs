// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.ViewModel.Base;
using Windows.System;

namespace Wfa.ViewModel.Tools
{
    /// <summary>
    /// 友链条目视图模型.
    /// </summary>
    public sealed class FriendLinkItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendLinkItemViewModel"/> class.
        /// </summary>
        public FriendLinkItemViewModel(string link, string name, string tag, string icon)
        {
            Link = link;
            Name = name;
            Tag = tag;
            Icon = icon;

            OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
        }

        /// <summary>
        /// 打开该链接的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenCommand { get; }

        /// <summary>
        /// 网址.
        /// </summary>
        [Reactive]
        public string Link { get; set; }

        /// <summary>
        /// 名称.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <summary>
        /// 标签.
        /// </summary>
        [Reactive]
        public string Tag { get; set; }

        /// <summary>
        /// 图标.
        /// </summary>
        [Reactive]
        public string Icon { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is FriendLinkItemViewModel model && Link == model.Link;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Link);

        private async Task OpenAsync()
        {
            if (!string.IsNullOrEmpty(Link))
            {
                await Launcher.LaunchUriAsync(new Uri(Link));
            }
        }
    }
}
