// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Windows.System;

namespace Wfa.ViewModel.Tools
{
    /// <summary>
    /// 工具条目视图模型.
    /// </summary>
    public sealed class ToolItemViewModel : ViewModelBase
    {
        private readonly IResourceToolkit _resourceToolkit;
        private readonly NavigationViewModel _navigationViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolItemViewModel"/> class.
        /// </summary>
        public ToolItemViewModel(IResourceToolkit resourceToolkit, NavigationViewModel navigationViewModel)
        {
            _resourceToolkit = resourceToolkit;
            _navigationViewModel = navigationViewModel;

            ActiveCommand = ReactiveCommand.CreateFromTask(ActiveAsync);
        }

        /// <summary>
        /// 激活命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <summary>
        /// 工具类型.
        /// </summary>
        [Reactive]
        public ToolType Type { get; set; }

        /// <summary>
        /// 工具名.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <summary>
        /// 设置工具类型.
        /// </summary>
        /// <param name="type">工具类型.</param>
        /// <exception cref="NotSupportedException">不受支持的工具类型.</exception>
        public void SetType(ToolType type)
        {
            Type = type;
            Name = type switch
            {
                ToolType.Translate => _resourceToolkit.GetLocaleString(LanguageNames.ZhEnTranslate),
                ToolType.FriendLinks => _resourceToolkit.GetLocaleString(LanguageNames.FriendsLink),
                ToolType.BiliSearch => _resourceToolkit.GetLocaleString(LanguageNames.BiliVideo),
                _ => throw new NotSupportedException(),
            };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ToolItemViewModel model && Type == model.Type;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Type);

        private async Task ActiveAsync()
        {
            if (Type == ToolType.BiliSearch)
            {
                await OpenBiliAsync();
            }
            else
            {
                var pageId = Type switch
                {
                    ToolType.Translate => PageIds.Translate,
                    ToolType.FriendLinks => PageIds.FriendLinks,
                    _ => throw new NotImplementedException(),
                };

                _navigationViewModel.NavigateToSecondaryView(pageId);
            }
        }

        private async Task OpenBiliAsync()
        {
            var isSupportBili = (await Launcher.FindUriSchemeHandlersAsync("richasy-bili")).Count > 0;
            if (isSupportBili)
            {
                await Launcher.LaunchUriAsync(new Uri("richasy-bili://find?keyword=Warframe"));
            }
            else
            {
                await Launcher.LaunchUriAsync(new Uri("https://search.bilibili.com/all?keyword=Warframe"));
            }
        }
    }
}
